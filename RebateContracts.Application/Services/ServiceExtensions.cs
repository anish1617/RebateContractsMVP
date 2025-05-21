using Microsoft.Extensions.DependencyInjection;

namespace RebateContracts.Application.Services;

/// <summary>
/// Extension methods for registering business logic services in the Application layer.
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Registers all rebate calculation and business logic services for DI.
    /// </summary>
    public static IServiceCollection AddRebateCalculationServices(this IServiceCollection services)
    {
        services.AddScoped<IPercentageRebateCalculatorService, PercentageRebateCalculatorService>();
        services.AddScoped<IPerMtRebateCalculatorService, PerMtRebateCalculatorService>();
        services.AddScoped<ITieredRebateCalculatorService, TieredRebateCalculatorService>();
        services.AddScoped<IRatePayableRebateCalculatorService, RatePayableRebateCalculatorService>();
        services.AddScoped<IConcentrationConversionService, ConcentrationConversionService>();
        services.AddScoped<IQuantityAdjustmentService, QuantityAdjustmentService>();
        services.AddScoped<IRebateCalculationOrchestrator, RebateCalculationOrchestrator>();
        return services;
    }
}
