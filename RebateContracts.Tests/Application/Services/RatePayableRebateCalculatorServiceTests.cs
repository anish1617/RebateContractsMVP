using RebateContracts.Application.Services;
using Xunit;
using System.Collections.Generic;

namespace RebateContracts.Tests.Application.Services;

public class RatePayableRebateCalculatorServiceTests
{
    private readonly RatePayableRebateCalculatorService _service = new();

    [Fact]
    public void Calculate_SingleRangeWithin_ReturnsCorrectRebate()
    {
        var ranges = new List<(decimal, decimal, decimal)>{ (0, 500, 0.02m) };
        var result = _service.Calculate(400, 10, ranges);
        Assert.Equal(400 * 10 * 0.02m, result);
    }

    [Fact]
    public void Calculate_MultipleRanges_PartialVolumeInSecondRange_ReturnsSum()
    {
        var ranges = new List<(decimal, decimal, decimal)>{ (0, 500, 0.02m), (500, 1000, 0.04m) };
        var result = _service.Calculate(600, 10, ranges);
        Assert.Equal(500*10*0.02m + 100*10*0.04m, result);
    }

    [Fact]
    public void Calculate_VolumeBelowFirstRange_ReturnsExpectedRebate()
    {
        var ranges = new List<(decimal, decimal, decimal)>{ (0, 500, 0.02m) };
        var result = _service.Calculate(100, 10, ranges);
        Assert.Equal(100 * 10 * 0.02m, result);
    }

    [Fact]
    public void Calculate_EmptyRanges_ReturnsZero()
    {
        var ranges = new List<(decimal, decimal, decimal)>();
        var result = _service.Calculate(100, 10, ranges);
        Assert.Equal(0, result);
    }
}
