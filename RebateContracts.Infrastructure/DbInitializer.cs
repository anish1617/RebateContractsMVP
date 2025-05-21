using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Text;
using RebateContracts.Domain.Entities;

namespace RebateContracts.Infrastructure;

public static class DbInitializer
{
    public static async Task SeedFromCsvAsync(IServiceProvider serviceProvider, string dataFolder)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<RebateContractsDbContext>();

        // 1. Seed BusinessUnits
        var businessUnitPath = Path.Combine(dataFolder, "BusinessUnits.csv");
        if (File.Exists(businessUnitPath))
        {
            var lines = await File.ReadAllLinesAsync(businessUnitPath, Encoding.UTF8);
            foreach (var line in lines.Skip(1))
            {
                var cols = line.Split(',');
                var name = cols[0].Trim();
                if (!context.BusinessUnits.Any(bu => bu.Name == name))
                    context.BusinessUnits.Add(new BusinessUnit { Id = Guid.NewGuid(), Name = name });
            }
            await context.SaveChangesAsync();
        }

        // 2. Seed Products
        var productPath = Path.Combine(dataFolder, "Products.csv");
        if (File.Exists(productPath))
        {
            var lines = await File.ReadAllLinesAsync(productPath, Encoding.UTF8);
            foreach (var line in lines.Skip(1))
            {
                var cols = line.Split(',');
                var name = cols[0].Trim();
                var globalCode = cols.Length > 1 ? cols[1].Trim() : string.Empty;
                if (!context.Products.Any(p => p.Name == name && p.GlobalCode == globalCode))
                    context.Products.Add(new Product { Id = Guid.NewGuid(), Name = name, GlobalCode = globalCode });
            }
            await context.SaveChangesAsync();
        }

        // 3. Seed Suppliers
        var supplierPath = Path.Combine(dataFolder, "Suppliers.csv");
        if (File.Exists(supplierPath))
        {
            var lines = await File.ReadAllLinesAsync(supplierPath, Encoding.UTF8);
            foreach (var line in lines.Skip(1))
            {
                var cols = line.Split(',');
                var name = cols[0].Trim();
                if (!context.Suppliers.Any(s => s.Name == name))
                    context.Suppliers.Add(new Supplier { Id = Guid.NewGuid(), Name = name });
            }
            await context.SaveChangesAsync();
        }

        // 4. Seed GlobalDemand
        var demandPath = Path.Combine(dataFolder, "Global_Demand.csv");
        if (File.Exists(demandPath))
        {
            var lines = await File.ReadAllLinesAsync(demandPath, Encoding.UTF8);
            foreach (var line in lines.Skip(1))
            {
                var cols = line.Split(',');
                var globalCode = cols[0].Trim();
                var productName = cols[1].Trim();
                var year = int.Parse(cols[2].Trim());
                var demand = decimal.Parse(cols[3].Trim(), CultureInfo.InvariantCulture);
                if (!context.GlobalDemands.Any(gd => gd.GlobalCode == globalCode && gd.Year == year))
                    context.GlobalDemands.Add(new GlobalDemand { Id = Guid.NewGuid(), GlobalCode = globalCode, ProductName = productName, Year = year, DemandInMT = demand });
            }
            await context.SaveChangesAsync();
        }

        // 5. Seed RebateContracts
        var contractPath = Path.Combine(dataFolder, "RebateContracts.csv");
        if (File.Exists(contractPath))
        {
            var lines = await File.ReadAllLinesAsync(contractPath, Encoding.UTF8);
            foreach (var line in lines.Skip(1))
            {
                var cols = line.Split(',');
                var name = cols[0].Trim();
                var type = Enum.Parse<RebateContractType>(cols[1].Trim());
                var start = DateTime.Parse(cols[2].Trim());
                var end = DateTime.Parse(cols[3].Trim());
                if (!context.RebateContracts.Any(rc => rc.Name == name && rc.StartDate == start))
                    context.RebateContracts.Add(new RebateContract { Id = Guid.NewGuid(), Name = name, ContractType = type, StartDate = start, EndDate = end });
            }
            await context.SaveChangesAsync();
        }

        // 6. Seed RebateRules
        var rulePath = Path.Combine(dataFolder, "RebateRules.csv");
        if (File.Exists(rulePath))
        {
            var lines = await File.ReadAllLinesAsync(rulePath, Encoding.UTF8);
            foreach (var line in lines.Skip(1))
            {
                var cols = line.Split(',');
                // Map contract, product, and set rule values as needed
                // Example: contractName, productName, percentage, perMT
                var contractName = cols[0].Trim();
                var productName = cols[1].Trim();
                decimal? percentage = null;
                if (!string.IsNullOrWhiteSpace(cols[2]))
                    percentage = decimal.Parse(cols[2], CultureInfo.InvariantCulture);
                decimal? perMT = null;
                if (!string.IsNullOrWhiteSpace(cols[3]))
                    perMT = decimal.Parse(cols[3], CultureInfo.InvariantCulture);
                var contract = context.RebateContracts.FirstOrDefault(rc => rc.Name == contractName);
                var product = context.Products.FirstOrDefault(p => p.Name == productName);
                if (contract != null && product != null && !context.RebateRules.Any(r => r.RebateContractId == contract.Id && r.ProductId == product.Id))
                {
                    context.RebateRules.Add(new RebateRule { Id = Guid.NewGuid(), RebateContractId = contract.Id, ProductId = product.Id, Percentage = percentage, PerMT = perMT });
                }
            }
            await context.SaveChangesAsync();
        }

        // 7. Seed TieredRebates
        var tierPath = Path.Combine(dataFolder, "TieredRebates.csv");
        if (File.Exists(tierPath))
        {
            var lines = await File.ReadAllLinesAsync(tierPath, Encoding.UTF8);
            foreach (var line in lines.Skip(1))
            {
                var cols = line.Split(',');
                // Example: contractName, productName, start, end, rate
                var contractName = cols[0].Trim();
                var productName = cols[1].Trim();
                var start = decimal.Parse(cols[2], CultureInfo.InvariantCulture);
                var end = decimal.Parse(cols[3], CultureInfo.InvariantCulture);
                var rate = decimal.Parse(cols[4], CultureInfo.InvariantCulture);
                var contract = context.RebateContracts.FirstOrDefault(rc => rc.Name == contractName);
                var product = context.Products.FirstOrDefault(p => p.Name == productName);
                var rule = contract != null && product != null ? context.RebateRules.FirstOrDefault(r => r.RebateContractId == contract.Id && r.ProductId == product.Id) : null;
                if (rule != null)
                {
                    context.TieredRebates.Add(new TieredRebate { Id = Guid.NewGuid(), RebateRuleId = rule.Id, StartVolume = start, EndVolume = end, Rate = rate });
                }
            }
            await context.SaveChangesAsync();
        }

        // 8. Seed ConcentrationConversions
        var convPath = Path.Combine(dataFolder, "Concentration_Conversion.csv");
        if (File.Exists(convPath))
        {
            var lines = await File.ReadAllLinesAsync(convPath, Encoding.UTF8);
            foreach (var line in lines.Skip(1))
            {
                var cols = line.Split(',');
                var contractName = cols[0].Trim();
                var origCode = cols[1].Trim();
                var multiplier = decimal.Parse(cols[2], CultureInfo.InvariantCulture);
                var targetCode = cols[3].Trim();
                var contract = context.RebateContracts.FirstOrDefault(rc => rc.Name == contractName);
                if (contract != null)
                {
                    context.ConcentrationConversions.Add(new ConcentrationConversion { Id = Guid.NewGuid(), RebateContractId = contract.Id, OriginalGlobalCode = origCode, ConversionMultiplier = multiplier, TargetGlobalCode = targetCode });
                }
            }
            await context.SaveChangesAsync();
        }

        // 9. Seed QuantityAdjustments
        var adjustPath = Path.Combine(dataFolder, "Rebate_Contract_Quantity_Adjust.csv");
        if (File.Exists(adjustPath))
        {
            var lines = await File.ReadAllLinesAsync(adjustPath, Encoding.UTF8);
            foreach (var line in lines.Skip(1))
            {
                var cols = line.Split(',');
                var contractName = cols[0].Trim();
                var globalCode = cols[1].Trim();
                var businessUnitName = cols[2].Trim();
                var year = int.Parse(cols[3].Trim());
                var qty = decimal.Parse(cols[4].Trim(), CultureInfo.InvariantCulture);
                var contract = context.RebateContracts.FirstOrDefault(rc => rc.Name == contractName);
                var bu = context.BusinessUnits.FirstOrDefault(b => b.Name == businessUnitName);
                if (contract != null && bu != null)
                {
                    context.QuantityAdjustments.Add(new QuantityAdjustment { Id = Guid.NewGuid(), RebateContractId = contract.Id, GlobalCode = globalCode, BusinessUnitId = bu.Id, Year = year, AdjustingQuantity = qty });
                }
            }
            await context.SaveChangesAsync();
        }

        // 10. Seed Sales
        var salesPath = Path.Combine(dataFolder, "Sales.csv");
        if (File.Exists(salesPath))
        {
            var lines = await File.ReadAllLinesAsync(salesPath, Encoding.UTF8);
            foreach (var line in lines.Skip(1))
            {
                var cols = line.Split(',');
                // Example: contractName, productName, businessUnitName, supplierName, date, volume, price
                var contractName = cols[0].Trim();
                var productName = cols[1].Trim();
                var businessUnitName = cols[2].Trim();
                var supplierName = cols[3].Trim();
                var date = DateTime.Parse(cols[4].Trim());
                var volume = decimal.Parse(cols[5].Trim(), CultureInfo.InvariantCulture);
                var price = decimal.Parse(cols[6].Trim(), CultureInfo.InvariantCulture);
                var contract = context.RebateContracts.FirstOrDefault(rc => rc.Name == contractName);
                var product = context.Products.FirstOrDefault(p => p.Name == productName);
                var bu = context.BusinessUnits.FirstOrDefault(b => b.Name == businessUnitName);
                var supplier = context.Suppliers.FirstOrDefault(s => s.Name == supplierName);
                if (contract != null && product != null && bu != null && supplier != null)
                {
                    context.Sales.Add(new Sale { Id = Guid.NewGuid(), RebateContractId = contract.Id, ProductId = product.Id, BusinessUnitId = bu.Id, SupplierId = supplier.Id, Date = date, Volume = volume, Price = price });
                }
            }
            await context.SaveChangesAsync();
        }
    }

    public static void SeedReferenceData(ModelBuilder modelBuilder)
    {
        // Example: Seed a default BusinessUnit (can be extended for more reference data)
        modelBuilder.Entity<BusinessUnit>().HasData(
            new BusinessUnit { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Default Unit" }
        );
        // Add HasData for other reference entities as needed
    }
}

// To seed from CSVs at startup (dev/ops), call:
// await DbInitializer.SeedFromCsvAsync(app.Services, "./datas");
// To seed reference data for Azure/production, use HasData in OnModelCreating (already configured).
// Extend DbInitializer for more entities and CSVs as needed.
