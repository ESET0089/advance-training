using SmartMeter.Data.Entities;
using System;

namespace SmartMeter.Data.Entities
{
    public class Arrears
    {
        public long ArrearId { get; set; }
        public long ConsumerId { get; set; }
        public Consumer Consumer { get; set; } = null!;
        public string ArrearType { get; set; } = null!; // 'Overdue','Penalty','Interest'
        public decimal Amount { get; set; }
        public string PaidStatus { get; set; } = null!; // 'Paid','Unpaid','Partially Paid'
        public long? BillId { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
