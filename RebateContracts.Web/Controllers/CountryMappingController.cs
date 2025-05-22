using Microsoft.AspNetCore.Mvc;
using RebateContracts.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RebateContracts.Web.Controllers;

public class CountryMappingController : Controller
{
    // TODO: Inject service for country mapping management

    public IActionResult Index()
    {
        // TODO: Fetch and display list of country mappings
        return View(new List<CountryMappingViewModel>());
    }

    public IActionResult Create()
    {
        return View(new CountryMappingViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CountryMappingViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        // await _service.CreateAsync(model);
        TempData["Toast"] = "Country mapping created successfully";
        await Task.CompletedTask;
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        // TODO: Fetch mapping and return edit form
        // var mapping = await _service.GetByIdAsync(id);
        // if (mapping == null) return NotFound();
        // return View(mapping);
        await Task.CompletedTask; // Add await to satisfy compiler
        return View("Create", new CountryMappingViewModel { Id = id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, CountryMappingViewModel model)
    {
        if (!ModelState.IsValid) return View("Create", model);
        // await _service.UpdateAsync(id, model);
        TempData["Toast"] = "Country mapping updated successfully";
        await Task.CompletedTask;
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        // await _service.DeleteAsync(id);
        TempData["Toast"] = "Country mapping deleted successfully";
        await Task.CompletedTask;
        return RedirectToAction(nameof(Index));
    }
}
