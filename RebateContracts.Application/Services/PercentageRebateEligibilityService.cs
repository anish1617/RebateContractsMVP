using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RebateContracts.Domain.Entities;
using RebateContracts.Infrastructure;

namespace RebateContracts.Application.Services;

public interface IPercentageRebateEligibilityService
{
    /// <summary>
    /// Determines if a sale is eligible for a percentage rebate, using rule, price, and share.
    /// </summary>
    Task<bool> IsEligibleAsync(PercentageRebateRule rule, decimal purchasedVolume, decimal avgPrice, string globalCode, int year, RebateContractsDbContext db);

    /// <summary>
    /// Calculates the rebate amount if eligible, otherwise returns 0.
    /// </summary>
    Task<decimal> CalculateRebateAsync(PercentageRebateRule rule, decimal purchasedVolume, decimal avgPrice, decimal invoicePrice, string globalCode, int year, RebateContractsDbContext db);
}

public class PercentageRebateEligibilityService : IPercentageRebateEligibilityService
{
    public async Task<bool> IsEligibleAsync(PercentageRebateRule rule, decimal purchasedVolume, decimal avgPrice, string globalCode, int year, RebateContractsDbContext db)
    {
        // Get global demand for share calculation if needed
        decimal? share = null;
        if (rule.MinShare.HasValue)
        {
            var demand = await db.GlobalDemands.FirstOrDefaultAsync(gd => gd.GlobalCode == globalCode && gd.Year == year);
            if (demand != null && demand.DemandInMT > 0)
                share = purchasedVolume / demand.DemandInMT;
        }
        return purchasedVolume > rule.VolumeThreshold &&
               (!rule.PriceThreshold.HasValue || avgPrice >= rule.PriceThreshold) &&
               (!rule.MinShare.HasValue || (share.HasValue && share.Value >= rule.MinShare));
    }

    public async Task<decimal> CalculateRebateAsync(PercentageRebateRule rule, decimal purchasedVolume, decimal avgPrice, decimal invoicePrice, string globalCode, int year, RebateContractsDbContext db)
    {
        if (await IsEligibleAsync(rule, purchasedVolume, avgPrice, globalCode, year, db))
            return decimal.Round(purchasedVolume * invoicePrice * rule.RebatePercent, 4);
        return 0;
    }
}
