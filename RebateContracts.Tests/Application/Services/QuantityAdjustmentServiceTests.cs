using RebateContracts.Application.Services;
using Xunit;

namespace RebateContracts.Tests.Application.Services;

public class QuantityAdjustmentServiceTests
{
    private readonly QuantityAdjustmentService _service = new();

    [Theory]
    [InlineData(100, 10, 110.000)] // positive adjustment
    [InlineData(100, -10, 90.000)] // negative adjustment
    [InlineData(0, 10, 10.000)]    // zero original
    [InlineData(100, 0, 100.000)]  // zero adjustment
    public void Adjust_ReturnsExpected(decimal original, decimal adjustment, decimal expected)
    {
        var result = _service.Adjust(original, adjustment);
        Assert.Equal(expected, result);
    }
}
