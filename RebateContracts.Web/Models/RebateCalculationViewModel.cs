using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RebateContracts.Domain.Entities;

namespace RebateContracts.Web.Models;

public class RebateCalculationViewModel
{
    [Required]
    public Guid ContractId { get; set; }
    [Required]
    public Guid ProductId { get; set; }
    [Required]
    public Guid BusinessUnitId { get; set; }
    [Required]
    public Guid SupplierId { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Volume must be greater than 0")] 
    public decimal Volume { get; set; }
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")] 
    public decimal Price { get; set; }
    public decimal? CalculatedRebate { get; set; }
    public string? Error { get; set; }

    // Dropdown data
    public List<RebateContract>? Contracts { get; set; }
    public List<Product>? Products { get; set; }
    public List<BusinessUnit>? BusinessUnits { get; set; }
    public List<Supplier>? Suppliers { get; set; }
}
