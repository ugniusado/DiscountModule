namespace DiscountModule.Core.Entities
{
    public class Package
    {
        public string Size { get; }
        public double Price { get; set; }

        public Package(string size, double price)
        {
            Size = size;
            Price = price;
        }
    }
}
