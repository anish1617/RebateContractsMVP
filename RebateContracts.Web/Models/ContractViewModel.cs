using System.ComponentModel.DataAnnotations;

namespace RebateContracts.Web.Models;

public class ContractViewModel
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int ContractType { get; set; } // Enum as int for now

    [Required]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
}
