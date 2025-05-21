using RebateContracts.Application.Services;
using Xunit;

namespace RebateContracts.Tests.Application.Services;

public class ConcentrationConversionServiceTests
{
    private readonly ConcentrationConversionService _service = new();

    [Theory]
    [InlineData(100, 10, 20, 200.000000)] // double concentration
    [InlineData(100, 20, 10, 50.000000)]  // half concentration
    [InlineData(100, 10, 0, 0.000000)]    // zero target
    [InlineData(100, 0, 10, 0.000000)]    // zero source
    [InlineData(0, 10, 20, 0.000000)]     // zero input
    public void Convert_ReturnsExpected(decimal input, decimal from, decimal to, decimal expected)
    {
        var result = _service.Convert(input, from, to);
        Assert.Equal(expected, result);
    }
}
