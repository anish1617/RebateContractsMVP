// Scaffolds interfaces for all main rebate calculation services in the Application layer.
// Each service will have a corresponding implementation and will be testable.

namespace RebateContracts.Application.Services;

/// <summary>
/// Calculates rebates for contracts with percentage-based rules.
/// </summary>
public interface IPercentageRebateCalculatorService
{
    /// <summary>
    /// Calculates the rebate amount based on a percentage rule.
    /// </summary>
    /// <param name="volume">The purchased volume.</param>
    /// <param name="price">The unit price.</param>
    /// <param name="rate">The rebate percentage (e.g., 0.05 for 5%).</param>
    /// <returns>The calculated rebate amount.</returns>
    decimal Calculate(decimal volume, decimal price, decimal rate);
}

/// <summary>
/// Calculates rebates for contracts with per-metric-ton (per-MT) rules.
/// </summary>
public interface IPerMtRebateCalculatorService
{
    /// <summary>
    /// Calculates the rebate amount based on a per-MT rule.
    /// </summary>
    /// <param name="volume">The purchased volume in metric tons.</param>
    /// <param name="ratePerMt">The rebate rate per metric ton.</param>
    /// <returns>The calculated rebate amount.</returns>
    decimal Calculate(decimal volume, decimal ratePerMt);
}

/// <summary>
/// Calculates rebates for contracts with tiered rules.
/// </summary>
public interface ITieredRebateCalculatorService
{
    /// <summary>
    /// Calculates the total rebate for a tiered contract.
    /// </summary>
    /// <param name="volume">The total purchased volume.</param>
    /// <param name="price">The unit price.</param>
    /// <param name="tiers">A list of (start, end, rate) tuples representing the tiers.</param>
    /// <returns>The total calculated rebate.</returns>
    decimal Calculate(decimal volume, decimal price, IReadOnlyList<(decimal Start, decimal End, decimal Rate)> tiers);
}

/// <summary>
/// Calculates rebates for contracts with rate payable rules (from rebate_rate_payable.csv).
/// </summary>
public interface IRatePayableRebateCalculatorService
{
    /// <summary>
    /// Calculates the total rebate for a rate payable contract.
    /// </summary>
    /// <param name="volume">The total purchased volume.</param>
    /// <param name="price">The unit price.</param>
    /// <param name="ranges">A list of (start, end, rate) tuples representing the ranges.</param>
    /// <returns>The total calculated rebate.</returns>
    decimal Calculate(decimal volume, decimal price, IReadOnlyList<(decimal Start, decimal End, decimal Rate)> ranges);
}

/// <summary>
/// Service for concentration conversion logic.
/// </summary>
public interface IConcentrationConversionService
{
    /// <summary>
    /// Converts a value from one concentration to another.
    /// </summary>
    /// <param name="inputValue">The value to convert.</param>
    /// <param name="fromConcentration">The source concentration.</param>
    /// <param name="toConcentration">The target concentration.</param>
    /// <returns>The converted value.</returns>
    decimal Convert(decimal inputValue, decimal fromConcentration, decimal toConcentration);
}

/// <summary>
/// Service for quantity adjustment logic.
/// </summary>
public interface IQuantityAdjustmentService
{
    /// <summary>
    /// Adjusts a quantity based on contract-specific rules.
    /// </summary>
    /// <param name="originalQuantity">The original quantity.</param>
    /// <param name="adjustment">The adjustment value.</param>
    /// <returns>The adjusted quantity.</returns>
    decimal Adjust(decimal originalQuantity, decimal adjustment);
}
