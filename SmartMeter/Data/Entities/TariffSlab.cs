namespace SmartMeter.Data.Entities
{
    public class TariffSlab
    {
        public int TariffSlabId { get; set; }
        public int TariffId { get; set; }
        public Tariff Tariff { get; set; } = null!;
        public decimal FromKwh { get; set; }
        public decimal ToKwh { get; set; }
        public decimal RatePerKwh { get; set; }
        public bool IsDeleted { get; set; }
    }
}
