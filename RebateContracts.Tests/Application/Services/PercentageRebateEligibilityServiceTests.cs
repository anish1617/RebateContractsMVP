using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RebateContracts.Application.Services;
using RebateContracts.Domain.Entities;
using RebateContracts.Infrastructure;
using Xunit;

namespace RebateContracts.Tests.Application.Services;

public class PercentageRebateEligibilityServiceTests : IDisposable
{
    private readonly RebateContractsDbContext _db;
    private readonly PercentageRebateEligibilityService _service;

    public PercentageRebateEligibilityServiceTests()
    {
        var options = new DbContextOptionsBuilder<RebateContractsDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _db = new RebateContractsDbContext(options);
        _service = new PercentageRebateEligibilityService();
    }

    [Fact]
    public async Task CalculateRebateAsync_HappyPath_AllConditionsMet_ReturnsExpectedRebate()
    {
        // Arrange
        var rule = new PercentageRebateRule
        {
            VolumeThreshold = 1000,
            PriceThreshold = 2.0m,
            MinShare = 0.1m,
            RebatePercent = 0.05m
        };
        decimal purchasedVolume = 1200;
        decimal avgPrice = 2.5m;
        decimal invoicePrice = 2.5m;
        string globalCode = "GC1";
        int year = 2024;
        _db.GlobalDemands.Add(new GlobalDemand { GlobalCode = globalCode, Year = year, DemandInMT = 8000 });
        await _db.SaveChangesAsync();

        // Act
        var rebate = await _service.CalculateRebateAsync(rule, purchasedVolume, avgPrice, invoicePrice, globalCode, year, _db);

        // Assert
        Assert.Equal(1200 * 2.5m * 0.05m, rebate);
    }

    [Fact]
    public async Task CalculateRebateAsync_NotEligible_VolumeBelowThreshold_ReturnsZero()
    {
        var rule = new PercentageRebateRule { VolumeThreshold = 1000, RebatePercent = 0.05m };
        decimal purchasedVolume = 900;
        decimal avgPrice = 2.5m;
        decimal invoicePrice = 2.5m;
        string globalCode = "GC1";
        int year = 2024;
        _db.GlobalDemands.Add(new GlobalDemand { GlobalCode = globalCode, Year = year, DemandInMT = 8000 });
        await _db.SaveChangesAsync();

        var rebate = await _service.CalculateRebateAsync(rule, purchasedVolume, avgPrice, invoicePrice, globalCode, year, _db);
        Assert.Equal(0, rebate);
    }

    [Fact]
    public async Task CalculateRebateAsync_NotEligible_PriceBelowThreshold_ReturnsZero()
    {
        var rule = new PercentageRebateRule { VolumeThreshold = 1000, PriceThreshold = 3.0m, RebatePercent = 0.05m };
        decimal purchasedVolume = 1200;
        decimal avgPrice = 2.5m;
        decimal invoicePrice = 2.5m;
        string globalCode = "GC1";
        int year = 2024;
        _db.GlobalDemands.Add(new GlobalDemand { GlobalCode = globalCode, Year = year, DemandInMT = 8000 });
        await _db.SaveChangesAsync();

        var rebate = await _service.CalculateRebateAsync(rule, purchasedVolume, avgPrice, invoicePrice, globalCode, year, _db);
        Assert.Equal(0, rebate);
    }

    [Fact]
    public async Task CalculateRebateAsync_NotEligible_ShareBelowThreshold_ReturnsZero()
    {
        var rule = new PercentageRebateRule { VolumeThreshold = 1000, MinShare = 0.5m, RebatePercent = 0.05m };
        decimal purchasedVolume = 1200;
        decimal avgPrice = 2.5m;
        decimal invoicePrice = 2.5m;
        string globalCode = "GC1";
        int year = 2024;
        _db.GlobalDemands.Add(new GlobalDemand { GlobalCode = globalCode, Year = year, DemandInMT = 8000 });
        await _db.SaveChangesAsync();

        var rebate = await _service.CalculateRebateAsync(rule, purchasedVolume, avgPrice, invoicePrice, globalCode, year, _db);
        Assert.Equal(0, rebate);
    }

    [Fact]
    public async Task CalculateRebateAsync_EdgeCase_VolumeEqualsThreshold_ReturnsZero()
    {
        var rule = new PercentageRebateRule { VolumeThreshold = 1200, RebatePercent = 0.05m };
        decimal purchasedVolume = 1200;
        decimal avgPrice = 2.5m;
        decimal invoicePrice = 2.5m;
        string globalCode = "GC1";
        int year = 2024;
        _db.GlobalDemands.Add(new GlobalDemand { GlobalCode = globalCode, Year = year, DemandInMT = 8000 });
        await _db.SaveChangesAsync();

        var rebate = await _service.CalculateRebateAsync(rule, purchasedVolume, avgPrice, invoicePrice, globalCode, year, _db);
        Assert.Equal(0, rebate);
    }

    [Fact]
    public async Task CalculateRebateAsync_EdgeCase_ShareEqualsThreshold_ReturnsRebate()
    {
        var rule = new PercentageRebateRule { VolumeThreshold = 1000, MinShare = 0.15m, RebatePercent = 0.05m };
        decimal purchasedVolume = 1200;
        decimal avgPrice = 2.5m;
        decimal invoicePrice = 2.5m;
        string globalCode = "GC1";
        int year = 2024;
        _db.GlobalDemands.Add(new GlobalDemand { GlobalCode = globalCode, Year = year, DemandInMT = 8000 }); // share = 1200/8000 = 0.15
        await _db.SaveChangesAsync();

        var rebate = await _service.CalculateRebateAsync(rule, purchasedVolume, avgPrice, invoicePrice, globalCode, year, _db);
        Assert.Equal(1200 * 2.5m * 0.05m, rebate);
    }

    [Fact]
    public async Task CalculateRebateAsync_EdgeCase_GlobalDemandZero_ReturnsZero()
    {
        var rule = new PercentageRebateRule { VolumeThreshold = 1000, MinShare = 0.1m, RebatePercent = 0.05m };
        decimal purchasedVolume = 1200;
        decimal avgPrice = 2.5m;
        decimal invoicePrice = 2.5m;
        string globalCode = "GC1";
        int year = 2024;
        _db.GlobalDemands.Add(new GlobalDemand { GlobalCode = globalCode, Year = year, DemandInMT = 0 });
        await _db.SaveChangesAsync();

        var rebate = await _service.CalculateRebateAsync(rule, purchasedVolume, avgPrice, invoicePrice, globalCode, year, _db);
        Assert.Equal(0, rebate);
    }

    [Fact]
    public async Task CalculateRebateAsync_NegativeOrZeroInputs_ReturnsZero()
    {
        var rule = new PercentageRebateRule { VolumeThreshold = 0, RebatePercent = 0.05m };
        decimal purchasedVolume = 0;
        decimal avgPrice = 0;
        decimal invoicePrice = 0;
        string globalCode = "GC1";
        int year = 2024;
        _db.GlobalDemands.Add(new GlobalDemand { GlobalCode = globalCode, Year = year, DemandInMT = 8000 });
        await _db.SaveChangesAsync();

        var rebate = await _service.CalculateRebateAsync(rule, purchasedVolume, avgPrice, invoicePrice, globalCode, year, _db);
        Assert.Equal(0, rebate);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
