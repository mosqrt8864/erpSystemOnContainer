using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Services;
using WebMVC.ViewModels.PurchaseRequest.List;
using WebMVC.ViewModels.PurchaseRequest.Create;
namespace WebMVC.Controllers;
public class PurchaseRequestController : Controller
{
    private readonly ILogger<PurchaseRequestController> _logger;
    private readonly IPurchaseRequestService _service;

    public PurchaseRequestController(ILogger<PurchaseRequestController> logger,IPurchaseRequestService service)
    {
        _logger = logger;
        _service = service;
    }
    public async Task<IActionResult> Index()
    {
        var purchaseRequests = await _service.GetPurchaseRequests(1,10);
        return View(purchaseRequests);
    }
    public async Task<IActionResult> Create(CreatePurchaseRequestViewModel purchaseRequest)
    {
        return View(purchaseRequest);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePurchaseRequest([FromBody]CreatePurchaseRequestViewModel purchaseRequest)
    {
        if (ModelState.IsValid)
        {
            await _service.Add(purchaseRequest);
            return Ok();
        }
        return BadRequest("參數驗證失敗");
    }
}