using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartMeter.Data.Entities;
using SmartMeter.Services;

namespace SmartMeter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) => _auth = auth;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                DisplayName = dto.DisplayName,
                Email = dto.Email,
                Phone = dto.Phone,
                Role = dto.Role ?? "Consumer",
                IsActive = true
            };

            var (success, error) = await _auth.RegisterAsync(user, dto.Password);
            if (!success) return BadRequest(new { error });
            return Ok(new { message = "Registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var (success, token, error) = await _auth.LoginAsync(dto.Username, dto.Password);
            if (!success) return Unauthorized(new { error });
            return Ok(new { token });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnly() => Ok(new { message = "Hello Admin" });
    }

    public record RegisterDto(string Username, string Password, string DisplayName, string? Email, string? Phone, string? Role);
    public record LoginDto(string Username, string Password);
}
