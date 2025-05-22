using System.ComponentModel.DataAnnotations;

namespace RebateContracts.Web.Models;

public class ConcentrationConversionViewModel
{
    public Guid Id { get; set; }

    [Required]
    public string RebateContract { get; set; } = string.Empty;

    [Required]
    public string OriginalGlobalCode { get; set; } = string.Empty;

    [Required]
    public decimal ConversionMultiplier { get; set; }

    [Required]
    public string TargetGlobalCode { get; set; } = string.Empty;
}
