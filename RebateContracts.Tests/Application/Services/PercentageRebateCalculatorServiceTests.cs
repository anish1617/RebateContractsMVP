using RebateContracts.Application.Services;
using Xunit;

namespace RebateContracts.Tests.Application.Services;

public class PercentageRebateCalculatorServiceTests
{
    private readonly PercentageRebateCalculatorService _service = new();

    [Theory]
    [InlineData(100, 10, 0.05, 50.0000)] // happy path
    [InlineData(0, 10, 0.05, 0.0000)]    // zero volume
    [InlineData(100, 0, 0.05, 0.0000)]   // zero price
    [InlineData(100, 10, 0, 0.0000)]     // zero rate
    [InlineData(100, 10, 1, 1000.0000)]  // 100% rate
    [InlineData(100, 10, -0.05, -50.0000)] // negative rate
    public void Calculate_ReturnsExpectedResult(decimal volume, decimal price, decimal rate, decimal expected)
    {
        var result = _service.Calculate(volume, price, rate);
        Assert.Equal(expected, result);
    }
}
