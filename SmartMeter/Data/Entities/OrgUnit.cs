namespace SmartMeter.Data.Entities
{
    public class OrgUnit
    {
        public int OrgUnitId { get; set; }
        public string Type { get; set; } = null!; // 'Zone','Substation','Feeder','DTR'
        public string Name { get; set; } = null!;
        public int? ParentId { get; set; }
        public OrgUnit? Parent { get; set; }
    }
}
