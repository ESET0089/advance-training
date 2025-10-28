using System;

namespace SmartMeter.Data.Entities
{
    public class Meter
    {
        public string MeterSerialNo { get; set; } = null!;
        public string IpAddress { get; set; } = null!;
        public string ICCID { get; set; } = null!;
        public string IMSI { get; set; } = null!;
        public string Manufacturer { get; set; } = null!;
        public string? Firmware { get; set; }
        public string Category { get; set; } = null!;
        public DateTimeOffset InstallTsUtc { get; set; }
        public string Status { get; set; } = "Active";
        public long? ConsumerId { get; set; }
        public Consumer? Consumer { get; set; }
    }
}
