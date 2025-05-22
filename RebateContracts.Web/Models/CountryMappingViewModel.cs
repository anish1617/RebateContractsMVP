using System.ComponentModel.DataAnnotations;

namespace RebateContracts.Web.Models;

public class CountryMappingViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Rebate Contract is required")]
    public string RebateContract { get; set; } = string.Empty;

    [Required(ErrorMessage = "Business Unit Name is required")]
    public string BusinessUnitName { get; set; } = string.Empty;
}
