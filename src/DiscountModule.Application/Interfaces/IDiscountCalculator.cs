using DiscountModule.Core.Entities;

namespace DiscountModule.Application.Interfaces
{
    public interface IDiscountCalculator
    {
        void CalculateDiscount(Shipment shipment);
    }
}
