using RebateContracts.Application.Services;
using Xunit;
using System.Collections.Generic;

namespace RebateContracts.Tests.Application.Services;

public class TieredRebateCalculatorServiceTests
{
    private readonly TieredRebateCalculatorService _service = new();

    [Fact]
    public void Calculate_SingleTierWithinRange_ReturnsCorrectRebate()
    {
        var tiers = new List<(decimal, decimal, decimal)>{ (0, 500, 0.05m) };
        var result = _service.Calculate(400, 10, tiers);
        Assert.Equal(400 * 10 * 0.05m, result);
    }

    [Fact]
    public void Calculate_MultipleTiers_PartialVolumeInSecondTier_ReturnsSum()
    {
        var tiers = new List<(decimal, decimal, decimal)>{ (0, 500, 0.05m), (500, 1000, 0.10m) };
        var result = _service.Calculate(600, 10, tiers);
        // 0-500: 500*10*0.05, 500-600: 100*10*0.10
        Assert.Equal(500*10*0.05m + 100*10*0.10m, result);
    }

    [Fact]
    public void Calculate_VolumeBelowFirstTier_ReturnsZero()
    {
        var tiers = new List<(decimal, decimal, decimal)>{ (100, 200, 0.05m) };
        var result = _service.Calculate(50, 10, tiers);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Calculate_EmptyTiers_ReturnsZero()
    {
        var result = _service.Calculate(100, 10, new List<(decimal, decimal, decimal)>());
        Assert.Equal(0, result);
    }
}
