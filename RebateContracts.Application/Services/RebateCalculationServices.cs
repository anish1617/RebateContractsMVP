using System.Collections.Generic;
using RebateContracts.Domain.Entities;

namespace RebateContracts.Application.Services;

/// <summary>
/// Default implementation for percentage-based rebate calculation.
/// </summary>
public class PercentageRebateCalculatorService : IPercentageRebateCalculatorService
{
    /// <summary>
    /// Calculates the rebate amount for percentage-based contracts as per RebateRules.csv.
    /// </summary>
    public decimal Calculate(decimal volume, decimal price, decimal rate)
    {
        // Business logic: rebate = volume × price × percentage
        return decimal.Round(volume * price * rate, 4);
    }
}

/// <summary>
/// Default implementation for per-metric-ton rebate calculation.
/// </summary>
public class PerMtRebateCalculatorService : IPerMtRebateCalculatorService
{
    /// <summary>
    /// Calculates the rebate amount for per-MT contracts as per RebateRules.csv.
    /// </summary>
    public decimal Calculate(decimal volume, decimal ratePerMt)
    {
        // Business logic: rebate = volume × perMT
        return decimal.Round(volume * ratePerMt, 4);
    }
}

/// <summary>
/// Default implementation for tiered rebate calculation.
/// </summary>
public class TieredRebateCalculatorService : ITieredRebateCalculatorService
{
    /// <summary>
    /// Calculates the total rebate for a tiered contract as per TieredRebates.csv.
    /// </summary>
    public decimal Calculate(decimal volume, decimal price, IReadOnlyList<(decimal Start, decimal End, decimal Rate)> tiers)
    {
        decimal total = 0;
        foreach (var (start, end, rate) in tiers)
        {
            if (volume > start)
            {
                var applicable = Math.Min(volume, end) - start;
                if (applicable > 0)
                    total += applicable * price * rate;
            }
        }
        return decimal.Round(total, 4);
    }
}

/// <summary>
/// Default implementation for rate payable rebate calculation.
/// </summary>
public class RatePayableRebateCalculatorService : IRatePayableRebateCalculatorService
{
    /// <summary>
    /// Calculates the total rebate for a rate payable contract as per rebate_rate_payable.csv.
    /// </summary>
    public decimal Calculate(decimal volume, decimal price, IReadOnlyList<(decimal Start, decimal End, decimal Rate)> ranges)
    {
        decimal total = 0;
        foreach (var (start, end, rate) in ranges)
        {
            if (volume > start)
            {
                var applicable = Math.Min(volume, end) - start;
                if (applicable > 0)
                    total += applicable * price * rate;
            }
        }
        return decimal.Round(total, 4);
    }
}

/// <summary>
/// Default implementation for concentration conversion.
/// </summary>
public class ConcentrationConversionService : IConcentrationConversionService
{
    /// <summary>
    /// Converts a value from one concentration to another as per Concentration_Conversion.csv.
    /// </summary>
    public decimal Convert(decimal inputValue, decimal fromConcentration, decimal toConcentration)
    {
        if (fromConcentration == 0) return 0;
        return decimal.Round(inputValue * toConcentration / fromConcentration, 6);
    }
}

/// <summary>
/// Default implementation for quantity adjustment.
/// </summary>
public class QuantityAdjustmentService : IQuantityAdjustmentService
{
    /// <summary>
    /// Adjusts a quantity based on contract-specific rules as per Rebate_Contract_Quantity_Adjust.csv.
    /// </summary>
    public decimal Adjust(decimal originalQuantity, decimal adjustment)
    {
        return decimal.Round(originalQuantity + adjustment, 3);
    }
}
