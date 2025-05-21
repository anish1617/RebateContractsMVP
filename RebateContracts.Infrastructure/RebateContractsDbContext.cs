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
    }
}
