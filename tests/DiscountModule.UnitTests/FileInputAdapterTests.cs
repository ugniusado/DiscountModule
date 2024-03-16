using DiscountModule.Infrastructure.Adapters;
using FluentAssertions;

namespace DiscountModule.Tests
{
    public class FileInputAdapterTests
    {
        [Fact]
        public void ReadShipments_ShouldCorrectlyParseValidLines()
        {
            // Arrange
            var path = Path.GetTempFileName();
            File.WriteAllLines(path, new[] { "2015-02-01 S LP", "2015-02-02 M MR" });

            // Act
            var shipments = FileInputAdapter.ReadShipments(path).ToList();

            // Assert
            shipments.Should().HaveCount(2);
            shipments[0].Package.Size.Should().Be("S");
            shipments[0].Provider.Should().Be("LP");
            shipments[1].Package.Size.Should().Be("M");
            shipments[1].Provider.Should().Be("MR");

            // Cleanup
            File.Delete(path);
        }

        [Fact]
        public void ReadShipments_ShouldHandleEmptyFile()
        {
            // Arrange
            var path = Path.GetTempFileName();
            File.WriteAllLines(path, Array.Empty<string>());

            // Act
            var shipments = FileInputAdapter.ReadShipments(path).ToList();

            // Assert
            shipments.Should().BeEmpty();

            // Cleanup
            File.Delete(path);
        }

        [Fact]
        public void ReadShipments_ShouldIgnoreInvalidLine()
        {
            // Arrange
            var path = Path.GetTempFileName();
            File.WriteAllLines(path, new[] { "Invalid Data Line" });

            // Act
            var shipments = FileInputAdapter.ReadShipments(path).ToList();

            // Assert
            shipments.Should().ContainSingle();
            shipments[0].IsIgnored.Should().BeTrue();

            // Cleanup
            File.Delete(path);
        }
    }
}
