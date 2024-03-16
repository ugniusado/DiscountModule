using DiscountModule.Core.Entities;

namespace DiscountModule.Infrastructure.Adapters
{
    public static class ConsoleOutputAdapter
    {
        public static void WriteShipment(Shipment shipment, double discount)
        {
            if (shipment.IsIgnored)
            {
                Console.WriteLine($"{shipment.RawLine} Ignored");
            }
            else
            {
                string discountStr = discount > 0 ? discount.ToString("F2") : "-";
                Console.WriteLine($"{shipment.Date:yyyy-MM-dd} {shipment.Package.Size} {shipment.Provider} {shipment.Package.Price:F2} {discountStr}");
            }
        }
    }
}
