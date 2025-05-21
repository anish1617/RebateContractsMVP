using Microsoft.EntityFrameworkCore;
using RebateContracts.Domain.Entities;

namespace RebateContracts.Infrastructure;

public class RebateContractsDbContext : DbContext
{
    public RebateContractsDbContext(DbContextOptions<RebateContractsDbContext> options) : base(options) { }

    public DbSet<RebateContract> RebateContracts => Set<RebateContract>();
    public DbSet<BusinessUnit> BusinessUnits => Set<BusinessUnit>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<RebateRule> RebateRules => Set<RebateRule>();
    public DbSet<TieredRebate> TieredRebates => Set<TieredRebate>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<GlobalDemand> GlobalDemands => Set<GlobalDemand>();
    public DbSet<ConcentrationConversion> ConcentrationConversions => Set<ConcentrationConversion>();
    public DbSet<QuantityAdjustment> QuantityAdjustments => Set<QuantityAdjustment>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<PercentageRebateRule> PercentageRebateRules => Set<PercentageRebateRule>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure many-to-many for RebateContract <-> BusinessUnit
        modelBuilder.Entity<RebateContract>()
            .HasMany(rc => rc.EligibleBusinessUnits)
            .WithMany(bu => bu.RebateContracts);

        // Configure one-to-many for RebateContract -> RebateRule
        modelBuilder.Entity<RebateContract>()
            .HasMany(rc => rc.RebateRules)
            .WithOne(rr => rr.RebateContract)
            .HasForeignKey(rr => rr.RebateContractId);

        // Configure one-to-many for Product -> RebateRule
        modelBuilder.Entity<Product>()
            .HasMany(p => p.RebateRules)
            .WithOne(rr => rr.Product)
            .HasForeignKey(rr => rr.ProductId);

        // Configure one-to-many for RebateRule -> TieredRebate
        modelBuilder.Entity<RebateRule>()
            .HasMany(rr => rr.Tiers)
            .WithOne(t => t.RebateRule)
            .HasForeignKey(t => t.RebateRuleId);

        // Set decimal precision for all relevant properties (MSSQL best practice)
        modelBuilder.Entity<ConcentrationConversion>()
            .Property(x => x.ConversionMultiplier)
            .HasPrecision(18, 6);
        modelBuilder.Entity<GlobalDemand>()
            .Property(x => x.DemandInMT)
            .HasPrecision(18, 3);
        modelBuilder.Entity<QuantityAdjustment>()
            .Property(x => x.AdjustingQuantity)
            .HasPrecision(18, 3);
        modelBuilder.Entity<RebateRule>()
            .Property(x => x.Percentage)
            .HasPrecision(8, 6);
        modelBuilder.Entity<RebateRule>()
            .Property(x => x.PerMT)
            .HasPrecision(18, 3);
        modelBuilder.Entity<Sale>()
            .Property(x => x.Price)
            .HasPrecision(18, 4);
        modelBuilder.Entity<Sale>()
            .Property(x => x.Volume)
            .HasPrecision(18, 3);
        modelBuilder.Entity<TieredRebate>()
            .Property(x => x.StartVolume)
            .HasPrecision(18, 3);
        modelBuilder.Entity<TieredRebate>()
            .Property(x => x.EndVolume)
            .HasPrecision(18, 3);
        modelBuilder.Entity<TieredRebate>()
            .Property(x => x.Rate)
            .HasPrecision(8, 6);
        modelBuilder.Entity<PercentageRebateRule>()
            .Property(x => x.RebatePercent)
            .HasPrecision(8, 6);
        modelBuilder.Entity<PercentageRebateRule>()
            .Property(x => x.VolumeThreshold)
            .HasPrecision(18, 3);
        modelBuilder.Entity<PercentageRebateRule>()
            .Property(x => x.PriceThreshold)
            .HasPrecision(18, 4);
        modelBuilder.Entity<PercentageRebateRule>()
            .Property(x => x.MinShare)
            .HasPrecision(8, 6);

        // Call static seeding logic for reference data
        DbInitializer.SeedReferenceData(modelBuilder);
    }
}
