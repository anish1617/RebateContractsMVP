using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RebateContracts.Domain.Entities;
using RebateContracts.Infrastructure;

namespace RebateContracts.Application.Services;

/// <summary>
/// Orchestrates rebate calculation for any contract type.
/// </summary>
public interface IRebateCalculationOrchestrator
{
    /// <summary>
    /// Calculates the rebate for a given contract, sale, and context.
    /// </summary>
    Task<decimal> CalculateRebateAsync(RebateContract contract, Sale sale, int year, RebateContractsDbContext db);
}

public class RebateCalculationOrchestrator : IRebateCalculationOrchestrator
{
    private readonly IPercentageRebateEligibilityService _percentageService;
    private readonly IPerMtRebateCalculatorService _perMtService;
    private readonly ITieredRebateCalculatorService _tieredService;
    private readonly IRatePayableRebateCalculatorService _ratePayableService;

    public RebateCalculationOrchestrator(
        IPercentageRebateEligibilityService percentageService,
        IPerMtRebateCalculatorService perMtService,
        ITieredRebateCalculatorService tieredService,
        IRatePayableRebateCalculatorService ratePayableService)
    {
        _percentageService = percentageService;
        _perMtService = perMtService;
        _tieredService = tieredService;
        _ratePayableService = ratePayableService;
    }

    public async Task<decimal> CalculateRebateAsync(RebateContract contract, Sale sale, int year, RebateContractsDbContext db)
    {
        switch (contract.ContractType)
        {
            case RebateContractType.Percentage:
                // Find matching PercentageRebateRule
                var rule = await db.PercentageRebateRules.FirstOrDefaultAsync(r => r.RebateContractId == contract.Id && r.GlobalCode == sale.ProductId.ToString() && r.ValidFrom <= sale.Date && r.ValidTo >= sale.Date);
                if (rule == null) return 0;
                return await _percentageService.CalculateRebateAsync(rule, sale.Volume, sale.Price, sale.Price, rule.GlobalCode, year, db);
            case RebateContractType.PerMT:
                // Find matching RebateRule for PerMT
                var perMtRule = await db.RebateRules.FirstOrDefaultAsync(r => r.RebateContractId == contract.Id && r.ProductId == sale.ProductId);
                if (perMtRule == null || perMtRule.PerMT == null) return 0;
                return _perMtService.Calculate(sale.Volume, perMtRule.PerMT.Value);
            case RebateContractType.Tiered:
                // Find matching RebateRule for Tiered
                var tieredRule = await db.RebateRules.Include(r => r.Tiers).FirstOrDefaultAsync(r => r.RebateContractId == contract.Id && r.ProductId == sale.ProductId);
                if (tieredRule == null || tieredRule.Tiers.Count == 0) return 0;
                var tiers = new List<(decimal, decimal, decimal)>();
                foreach (var t in tieredRule.Tiers)
                    tiers.Add((t.StartVolume, t.EndVolume, t.Rate));
                return _tieredService.Calculate(sale.Volume, sale.Price, tiers);
            // If RatePayable is a future contract type, add logic here:
            // case RebateContractType.RatePayable:
            //     // Find matching RebateRule for RatePayable
            //     var ratePayableRule = await db.RebateRules.Include(r => r.Tiers).FirstOrDefaultAsync(r => r.RebateContractId == contract.Id && r.ProductId == sale.ProductId);
            //     if (ratePayableRule == null || ratePayableRule.Tiers.Count == 0) return 0;
            //     var ranges = new List<(decimal, decimal, decimal)>();
            //     foreach (var t in ratePayableRule.Tiers)
            //         ranges.Add((t.StartVolume, t.EndVolume, t.Rate));
            //     return _ratePayableService.Calculate(sale.Volume, sale.Price, ranges);
            default:
                return 0;
        }
    }
}
