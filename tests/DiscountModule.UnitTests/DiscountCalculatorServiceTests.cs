using AutoFixture.Xunit2;
using DiscountModule.Application.Services;
using DiscountModule.Core.Entities;
using DiscountModule.Core.Enums;
using FluentAssertions;

namespace DiscountModule.UnitTests
{
    public class DiscountCalculatorServiceTests
    {
        [Theory, AutoData]
        public void CalculateDiscount_ShouldApplyCorrectDiscount_ForSmallPackage(DiscountCalculatorService service, DateTime date)
        {
            // Arrange
            var package = new Package(PackageSize.S.ToString(), 2.00);
            var shipment = new Shipment(date, package, Carrier.MR.ToString());

            // Act
            service.CalculateDiscount(shipment);

            // Assert
            shipment.Package.Price.Should().Be(1.50);
            shipment.Discount.Should().Be(0.50);
        }

        [Fact]
        public void CalculateDiscount_ShouldNotApplyDiscount_WhenPackageIsLargeAndNotThirdInMonth()
        {
            // Arrange
            var service = new DiscountCalculatorService();
            var package = new Package(PackageSize.L.ToString(), 6.90);
            var shipment = new Shipment(new DateTime(2023, 1, 15), package, Carrier.LP.ToString());

            // Act
            service.CalculateDiscount(shipment);

            // Assert
            shipment.Package.Price.Should().Be(6.90);
            shipment.Discount.Should().Be(0);
        }

        [Fact]
        public void CalculateDiscount_ShouldApplyNoDiscount_ForMediumPackage()
        {
            // Arrange
            var service = new DiscountCalculatorService();
            var package = new Package(PackageSize.M.ToString(), 4.90);
            var shipment = new Shipment(new DateTime(2023, 1, 1), package, Carrier.LP.ToString());

            // Act
            service.CalculateDiscount(shipment);

            // Assert
            shipment.Package.Price.Should().Be(4.90);
            shipment.Discount.Should().Be(0);
        }

        [Fact]
        public void CalculateDiscount_ShouldResetDiscount_ForNewMonth()
        {
            // Arrange
            var service = new DiscountCalculatorService();
            var package1 = new Package(PackageSize.L.ToString(), 6.90);
            var shipment1 = new Shipment(new DateTime(2023, 1, 31), package1, Carrier.LP.ToString());

            // Act
            service.CalculateDiscount(shipment1);
            service.CalculateDiscount(shipment1);
            service.CalculateDiscount(shipment1);

            var package2 = new Package(PackageSize.L.ToString(), 6.90);
            var shipment2 = new Shipment(new DateTime(2023, 2, 1), package2, Carrier.LP.ToString());

            service.CalculateDiscount(shipment2);

            // Assert
            shipment2.Package.Price.Should().Be(6.90);
            shipment2.Discount.Should().Be(0);
        }
    }
}
