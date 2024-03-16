using DiscountModule.Application.Interfaces;
using DiscountModule.Core.Entities;
using DiscountModule.Core.Enums;

namespace DiscountModule.Application.Services
{
    public class DiscountCalculatorService : IDiscountCalculator
    {
        private const double MaxMonthlyDiscount = 10.00;
        private readonly Dictionary<DateTime, double> _monthlyDiscounts = new();
        private readonly Dictionary<DateTime, int> _lpLargePackageCounts = new();

        public void CalculateDiscount(Shipment shipment)
        {
            if (shipment == null || shipment.IsIgnored || shipment.Package == null)
            {
                return;
            }

            double discount = 0.0;

            if (!_lpLargePackageCounts.ContainsKey(shipment.Date))
            {
                _lpLargePackageCounts[shipment.Date] = 0;
            }

            if (shipment.Provider == Carrier.LP.ToString() && shipment.Package.Size == PackageSize.L.ToString())
            {
                _lpLargePackageCounts[shipment.Date]++;
                if (_lpLargePackageCounts[shipment.Date] % 3 == 0)
                {
                    discount = Math.Min(shipment.Package.Price, MaxMonthlyDiscount - GetMonthlyDiscount(shipment.Date));
                    shipment.Package.Price -= discount;
                }
            }
            else if (shipment.Package.Size == PackageSize.S.ToString())
            {
                double lowestSPrice = 1.50;
                if (shipment.Package.Price > lowestSPrice)
                {
                    discount = Math.Min(shipment.Package.Price - lowestSPrice, MaxMonthlyDiscount - GetMonthlyDiscount(shipment.Date));
                    shipment.Package.Price = lowestSPrice;
                }
            }

            shipment.Discount = discount;
            UpdateMonthlyDiscount(shipment.Date, discount);
        }

        private double GetMonthlyDiscount(DateTime date)
        {
            _monthlyDiscounts.TryGetValue(date.Date, out var discount);
            return discount;
        }

        private void UpdateMonthlyDiscount(DateTime date, double discount)
        {
            if (!_monthlyDiscounts.ContainsKey(date))
            {
                _monthlyDiscounts[date] = 0;
            }

            _monthlyDiscounts[date] += discount;
        }
    }
}
