using System;

namespace SmartMeter.Data.Entities
{
    public class User
    {
        public long UserId { get; set; }
        public string Username { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTimeOffset? LastLoginUtc { get; set; }
        public bool IsActive { get; set; } = true;

        // Added to support role-based authorization (e.g. "Admin", "Consumer")
        public string Role { get; set; } = "Consumer";
    }
}
