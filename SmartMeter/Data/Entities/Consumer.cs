using System;

namespace SmartMeter.Data.Entities
{
    public class Consumer
    {
        public long ConsumerId { get; set; }
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int OrgUnitId { get; set; }
        public OrgUnit OrgUnit { get; set; } = null!;
        public int TariffId { get; set; }
        public Tariff Tariff { get; set; } = null!;
        public string Status { get; set; } = "Active";
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public string CreatedBy { get; set; } = "system";
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
