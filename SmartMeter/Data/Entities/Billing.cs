using SmartMeter.Data.Entities;
using System;

namespace SmartMeterApi.Data.Entities
{
    public class Billing
    {
        public long BillId { get; set; }
        public long ConsumerId { get; set; }
        public Consumer Consumer { get; set; } = null!;
        public string MeterId { get; set; } = null!; // MeterSerialNo
        public DateOnly BillingPeriodStart { get; set; }
        public DateOnly BillingPeriodEnd { get; set; }
        public decimal TotalUnitsConsumed { get; set; }
        public decimal BaseAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTimeOffset GeneratedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateOnly DueDate { get; set; }
        public DateTimeOffset? PaidDate { get; set; }
        public string PaymentStatus { get; set; } = "Unpaid";
        public DateTimeOffset? DisconnectionDate { get; set; }
    }
}
