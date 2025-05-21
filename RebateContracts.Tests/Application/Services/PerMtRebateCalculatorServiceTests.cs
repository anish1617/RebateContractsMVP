using RebateContracts.Application.Services;
using Xunit;

namespace RebateContracts.Tests.Application.Services;

public class PerMtRebateCalculatorServiceTests
{
    private readonly PerMtRebateCalculatorService _service = new();

    [Theory]
    [InlineData(100, 50, 5000.0000)] // happy path
    [InlineData(0, 50, 0.0000)]      // zero volume
    [InlineData(100, 0, 0.0000)]     // zero rate
    [InlineData(100, -50, -5000.0000)] // negative rate
    public void Calculate_ReturnsExpectedResult(decimal volume, decimal ratePerMt, decimal expected)
    {
        var result = _service.Calculate(volume, ratePerMt);
        Assert.Equal(expected, result);
    }
}
