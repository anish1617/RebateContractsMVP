using Microsoft.AspNetCore.Mvc;
using RebateContracts.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RebateContracts.Web.Controllers;

public class QuantityAdjustmentController : Controller
{
    // TODO: Inject service for quantity adjustment management

    public IActionResult Index()
    {
        // TODO: Fetch and display list of adjustments
        return View(new List<QuantityAdjustmentViewModel>());
    }

    public IActionResult Create()
    {
        return View(new QuantityAdjustmentViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(QuantityAdjustmentViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        // await _service.CreateAsync(model);
        TempData["Toast"] = "Quantity adjustment created successfully";
        await Task.CompletedTask;
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        // TODO: Fetch adjustment and return edit form
        // var adjustment = await _service.GetByIdAsync(id);
        // if (adjustment == null) return NotFound();
        // return View(adjustment);
        await Task.CompletedTask; // Add await to satisfy compiler
        return View("Create", new QuantityAdjustmentViewModel { Id = id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, QuantityAdjustmentViewModel model)
    {
        if (!ModelState.IsValid) return View("Create", model);
        // await _service.UpdateAsync(id, model);
        TempData["Toast"] = "Quantity adjustment updated successfully";
        await Task.CompletedTask;
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        // await _service.DeleteAsync(id);
        TempData["Toast"] = "Quantity adjustment deleted successfully";
        await Task.CompletedTask;
        return RedirectToAction(nameof(Index));
    }
}
