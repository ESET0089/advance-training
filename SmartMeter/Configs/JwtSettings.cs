namespace SmartMeter.Configs
{
    public class JwtSettings
    {
        public string Secret { get; set; } = null!;
        public int ExpiryMinutes { get; set; } = 120;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
    }
}
