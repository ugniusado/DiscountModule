namespace DiscountModule.Core.Entities
{
    public class Shipment
    {
        public DateTime Date { get; }
        public Package Package { get; }
        public string Provider { get; }
        public double Discount { get; set; }
        public bool IsIgnored { get; set; }
        public string RawLine { get; set; }

        public Shipment(DateTime date, Package package, string provider, string rawLine = "", bool isIgnored = false)
        {
            Date = date;
            Package = package;
            Provider = provider;
            IsIgnored = isIgnored;
            RawLine = rawLine;
        }
    }
}
