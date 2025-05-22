using System.ComponentModel.DataAnnotations;

namespace RebateContracts.Web.Models;

public class QuantityAdjustmentViewModel
{
    public Guid Id { get; set; }

    [Required]
    public string RebateContract { get; set; } = string.Empty;

    [Required]
    public string GlobalCode { get; set; } = string.Empty;

    [Required]
    public string BusinessUnit { get; set; } = string.Empty;

    [Required]
    public int Year { get; set; }

    [Required]
    public decimal AdjustingQuantity { get; set; }
}
