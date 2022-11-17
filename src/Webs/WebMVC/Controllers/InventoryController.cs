using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Services;
using WebMVC.ViewModels;
namespace WebMVC.Controllers;

public class InventoryController : Controller
{
    private readonly ILogger<InventoryController> _logger;
    private IInventoryService _inventorySrv;

    public InventoryController(ILogger<InventoryController> logger,IInventoryService inventorySrv)
    {
        _logger = logger;
        _inventorySrv = inventorySrv;
    }

    public async Task<IActionResult> Index()
    {
        var partNumbers = await _inventorySrv.GetPartNumbers(1,10);
        return View(partNumbers);
    }
    public async Task<IActionResult> Details(string id)
    {
        Console.WriteLine("Hello");
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }
        Console.WriteLine(id);
        var partNumber = await _inventorySrv.GetPartNumber(id);
        Console.WriteLine(partNumber);
        if (partNumber == null)
        {
            return NotFound();
        }
        return View(partNumber);
    }

    public async Task<IActionResult> Create(PartNumber partNumber)
    {
        Console.WriteLine("Create");
        if (ModelState.IsValid)
        {
            await _inventorySrv.AddPartNumber(partNumber);
            return RedirectToAction(nameof(Index));
        }
        return View(partNumber);
    }

    public async Task<IActionResult> Edit(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }
        var partNumber = await _inventorySrv.GetPartNumber(id);
        if (partNumber == null)
        {
            return NotFound();
        }
        return View(partNumber);
    }
    [HttpPost]
    public async Task<ActionResult> Edit(PartNumber partNumber)
    {
        Console.WriteLine("Edit");
        if (ModelState.IsValid) { 
            await _inventorySrv.UpdatePartNumber(partNumber);
            return RedirectToAction("Index");
        }
        return View(partNumber);
    }
}