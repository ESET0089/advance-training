using SmartMeter.Data.Entities;

namespace SmartMeter.Services
{
    public interface IAuthService
    {
        Task<(bool Success, string? Token, string? Error)> LoginAsync(string username, string password);
        Task<(bool Success, string? Error)> RegisterAsync(User user, string password);
    }
}
