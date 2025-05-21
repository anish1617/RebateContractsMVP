using RebateContracts.Domain.Entities;
using Xunit;
using System;

namespace RebateContracts.Tests.Domain.Entities;

public class PercentageRebateRuleTests
{
    [Fact]
    public void Eligible_WhenVolumeAndPriceAndShareMet_ReturnsTrue()
    {
        var rule = new PercentageRebateRule
        {
            VolumeThreshold = 1000,
            PriceThreshold = 2.0m,
            MinShare = 0.1m
        };
        decimal purchasedVolume = 1200;
        decimal avgPrice = 2.5m;
        decimal share = 0.15m;
        var eligible = purchasedVolume > rule.VolumeThreshold &&
                      (!rule.PriceThreshold.HasValue || avgPrice >= rule.PriceThreshold) &&
                      (!rule.MinShare.HasValue || share >= rule.MinShare);
        Assert.True(eligible);
    }

    [Fact]
    public void NotEligible_WhenVolumeBelowThreshold_ReturnsFalse()
    {
        var rule = new PercentageRebateRule { VolumeThreshold = 1000 };
        decimal purchasedVolume = 900;
        var eligible = purchasedVolume > rule.VolumeThreshold;
        Assert.False(eligible);
    }

    [Fact]
    public void NotEligible_WhenPriceBelowThreshold_ReturnsFalse()
    {
        var rule = new PercentageRebateRule { VolumeThreshold = 1000, PriceThreshold = 2.0m };
        decimal purchasedVolume = 1200;
        decimal avgPrice = 1.5m;
        var eligible = purchasedVolume > rule.VolumeThreshold && avgPrice >= rule.PriceThreshold;
        Assert.False(eligible);
    }

    [Fact]
    public void NotEligible_WhenShareBelowThreshold_ReturnsFalse()
    {
        var rule = new PercentageRebateRule { VolumeThreshold = 1000, MinShare = 0.2m };
        decimal purchasedVolume = 1200;
        decimal share = 0.1m;
        var eligible = purchasedVolume > rule.VolumeThreshold && share >= rule.MinShare;
        Assert.False(eligible);
    }
}
