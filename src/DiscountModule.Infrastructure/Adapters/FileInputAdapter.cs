using DiscountModule.Core.Entities;
using DiscountModule.Core.Enums;

namespace DiscountModule.Infrastructure.Adapters
{
    public static class FileInputAdapter
    {
        public static IEnumerable<Shipment> ReadShipments(string filePath)
        {
            var shipments = new List<Shipment>();

            var lines = File.ReadAllLines(filePath);
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var parts = line.Split(' ');
                if (parts.Length != 3 || !DateTime.TryParse(parts[0], out var date) ||
                    !Enum.TryParse(parts[1], true, out PackageSize size) ||
                    !Enum.TryParse(parts[2], true, out Carrier carrier))
                {
                    shipments.Add(new Shipment(DateTime.MinValue, null, null, line, true));
                    continue;
                }

                var package = new Package(size.ToString(), GetInitialPrice(size, carrier));
                shipments.Add(new Shipment(date, package, carrier.ToString()));
            }

            return shipments;
        }

        private static double GetInitialPrice(PackageSize size, Carrier carrier)
        {
            var prices = new Dictionary<Carrier, Dictionary<PackageSize, double>>
            {
                {
                    Carrier.LP, new Dictionary<PackageSize, double>
                    {
                        { PackageSize.S, 1.50 },
                        { PackageSize.M, 4.90 },
                        { PackageSize.L, 6.90 }
                    }
                },
                {
                    Carrier.MR, new Dictionary<PackageSize, double>
                    {
                        { PackageSize.S, 2.00 },
                        { PackageSize.M, 3.00 },
                        { PackageSize.L, 4.00 }
                    }
                }
            };

            return prices.TryGetValue(carrier, out var carrierPricing) && carrierPricing.TryGetValue(size, out var price) ? price : 0;
        }
    }
}
