using System;

namespace SmartMeter.Data.Entities
{
    public class TodRule
    {
        public int TodRuleId { get; set; }
        public int TariffId { get; set; }
        public Tariff Tariff { get; set; } = null!;
        public string Name { get; set; } = null!;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public decimal RatePerKwh { get; set; }
        public bool IsDeleted { get; set; }
    }
}
