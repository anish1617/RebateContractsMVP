using Microsoft.AspNetCore.Mvc;
using RebateContracts.Application.Services;
using RebateContracts.Web.Models;
using System.Threading.Tasks;

namespace RebateContracts.Web.Controllers;

public class ContractsController : Controller
{
    // TODO: Inject services for contract management (e.g., IContractService)

    public IActionResult Index()
    {
        // TODO: Fetch and display list of contracts
        return View();
    }

    public IActionResult Create()
    {
        // Return contract creation form
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ContractViewModel model)
    {
        // TODO: Validate and create contract
        if (!ModelState.IsValid) return View(model);
        // await _contractService.CreateAsync(model);
        await Task.CompletedTask;
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(Guid id)
    {
        // TODO: Fetch contract and return edit form
        // var contract = await _contractService.GetByIdAsync(id);
        // if (contract == null) return NotFound();
        // return View(contract);
        ViewBag.Id = id;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ContractViewModel model)
    {
        // TODO: Validate and update contract
        if (!ModelState.IsValid) return View(model);
        // await _contractService.UpdateAsync(id, model);
        await Task.CompletedTask;
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        // TODO: Delete contract
        // await _contractService.DeleteAsync(id);
        await Task.CompletedTask;
        return RedirectToAction(nameof(Index));
    }
}
