using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RebateContracts.Infrastructure;
using RebateContracts.Application.Services;
using RebateContracts.Domain.Entities;
using RebateContracts.Web.Models;

namespace RebateContracts.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRebateCalculationOrchestrator _orchestrator;
    private readonly RebateContractsDbContext _db;

    public HomeController(ILogger<HomeController> logger, IRebateCalculationOrchestrator orchestrator, RebateContractsDbContext db)
    {
        _logger = logger;
        _orchestrator = orchestrator;
        _db = db;
    }

    public IActionResult Index()
    {
        var model = new RebateCalculationViewModel
        {
            Date = DateTime.Today,
            Contracts = _db.RebateContracts.ToList(),
            Products = _db.Products.ToList(),
            BusinessUnits = _db.BusinessUnits.ToList(),
            Suppliers = _db.Suppliers.ToList()
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Index(RebateCalculationViewModel model)
    {
        model.Contracts = _db.RebateContracts.ToList();
        model.Products = _db.Products.ToList();
        model.BusinessUnits = _db.BusinessUnits.ToList();
        model.Suppliers = _db.Suppliers.ToList();
        if (!ModelState.IsValid)
            return View(model);
        try
        {
            var contract = await _db.RebateContracts.FindAsync(model.ContractId);
            if (contract == null) { model.Error = "Contract not found."; return View(model); }
            var sale = new Sale
            {
                RebateContractId = model.ContractId,
                ProductId = model.ProductId,
                BusinessUnitId = model.BusinessUnitId,
                SupplierId = model.SupplierId,
                Date = model.Date,
                Volume = model.Volume,
                Price = model.Price
            };
            var rebate = await _orchestrator.CalculateRebateAsync(contract, sale, model.Date.Year, _db);
            model.CalculatedRebate = rebate;
        }
        catch (Exception ex)
        {
            model.Error = ex.Message;
        }
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
