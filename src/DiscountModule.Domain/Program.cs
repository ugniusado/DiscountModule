using DiscountModule.Application.Exceptions;
using DiscountModule.Application.Services;
using DiscountModule.Infrastructure.Adapters;

var filePath = @"InputFiles\input.txt";

try
{
    var shipments = FileInputAdapter.ReadShipments(filePath);
    var discountCalculator = new DiscountCalculatorService();

    foreach (var shipment in shipments)
    {
        discountCalculator.CalculateDiscount(shipment);
        ConsoleOutputAdapter.WriteShipment(shipment, shipment.Discount);
    }
}
catch (FileReadException e)
{
    Console.WriteLine(e.Message);
}
catch (InvalidInputException e)
{
    Console.WriteLine(e.Message);
}
catch (Exception e)
{
    Console.WriteLine($"An unexpected error occurred: {e.Message}");
}
