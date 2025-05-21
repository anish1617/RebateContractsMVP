using System.ComponentModel.DataAnnotations;

namespace RebateContracts.Web.Models;

public class GlobalDemandViewModel
{
    public Guid Id { get; set; }

    [Required]
    public string Product { get; set; } = string.Empty;

    [Required]
    public int Year { get; set; }

    [Required]
    public decimal Forecast { get; set; }
}
