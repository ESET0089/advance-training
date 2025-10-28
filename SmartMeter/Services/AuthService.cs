using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartMeter.Configs;
using SmartMeter.Data.Context;
using SmartMeter.Data.Entities;
using SmartMeter.Services;


namespace SmartMeter.Services
{
    public class AuthService : IAuthService
    {
        private readonly SmartMeterDbContext _db;
        private readonly JwtSettings _jwt;

        public AuthService(SmartMeterDbContext db, IOptions<JwtSettings> jwtOptions)
        {
            _db = db;
            _jwt = jwtOptions.Value;
        }

        public async Task<(bool Success, string? Error)> RegisterAsync(User user, string password)
        {
            if (await _db.Users.AnyAsync(u => u.Username == user.Username))
                return (false, "Username already exists");

            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = Encoding.UTF8.GetBytes(hash);

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return (true, null);
        }

        public async Task<(bool Success, string? Token, string? Error)> LoginAsync(string username, string password)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return (false, null, "Invalid credentials");

            var storedHash = Encoding.UTF8.GetString(user.PasswordHash);
            if (!BCrypt.Net.BCrypt.Verify(password, storedHash))
                return (false, null, "Invalid credentials");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwt.Secret);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwt.ExpiryMinutes),
                Issuer = _jwt.Issuer,
                Audience = _jwt.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.LastLoginUtc = DateTimeOffset.UtcNow;
            await _db.SaveChangesAsync();

            return (true, tokenHandler.WriteToken(token), null);
        }
    }
}
