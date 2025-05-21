using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RebateContracts.Domain.Entities;

public enum RebateContractType
{
    Percentage,
    PerMT,
    Tiered
}

public class RebateContract
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public RebateContractType ContractType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<BusinessUnit> EligibleBusinessUnits { get; set; } = new List<BusinessUnit>();
    public ICollection<RebateRule> RebateRules { get; set; } = new List<RebateRule>();
}

public class BusinessUnit
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public ICollection<RebateContract> RebateContracts { get; set; } = new List<RebateContract>();
}

public class RebateRule
{
    [Key]
    public Guid Id { get; set; }
    public Guid RebateContractId { get; set; }
    public RebateContract RebateContract { get; set; } = null!;
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public decimal? Percentage { get; set; }
    public decimal? PerMT { get; set; }
    public ICollection<TieredRebate> Tiers { get; set; } = new List<TieredRebate>();
}

public class Product
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string GlobalCode { get; set; } = string.Empty;
    public ICollection<RebateRule> RebateRules { get; set; } = new List<RebateRule>();
}

public class TieredRebate
{
    [Key]
    public Guid Id { get; set; }
    public Guid RebateRuleId { get; set; }
    public RebateRule RebateRule { get; set; } = null!;
    public decimal StartVolume { get; set; }
    public decimal EndVolume { get; set; }
    public decimal Rate { get; set; }
}

public class Supplier
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
}

public class GlobalDemand
{
    [Key]
    public Guid Id { get; set; }
    public string GlobalCode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal DemandInMT { get; set; }
}

public class ConcentrationConversion
{
    [Key]
    public Guid Id { get; set; }
    public Guid RebateContractId { get; set; }
    public string OriginalGlobalCode { get; set; } = string.Empty;
    public string TargetGlobalCode { get; set; } = string.Empty;
    public decimal ConversionMultiplier { get; set; }
}

public class QuantityAdjustment
{
    [Key]
    public Guid Id { get; set; }
    public Guid RebateContractId { get; set; }
    public string GlobalCode { get; set; } = string.Empty;
    public Guid BusinessUnitId { get; set; }
    public int Year { get; set; }
    public decimal AdjustingQuantity { get; set; }
}

public class Sale
{
    [Key]
    public Guid Id { get; set; }
    public Guid RebateContractId { get; set; }
    public Guid ProductId { get; set; }
    public Guid BusinessUnitId { get; set; }
    public Guid SupplierId { get; set; }
    public DateTime Date { get; set; }
    public decimal Volume { get; set; }
    public decimal Price { get; set; }
}
