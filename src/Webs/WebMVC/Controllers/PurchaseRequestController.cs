using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Services;
using WebMVC.ViewModels.PurchaseRequest.List;
using WebMVC.ViewModels.PurchaseRequest.Create;
using WebMVC.ViewModels.PurchaseRequest.Update;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace WebMVC.Controllers;
public class PurchaseRequestController : Controller
{
    private readonly ILogger<PurchaseRequestController> _logger;
    private readonly IPurchaseRequestService _service;
    private readonly IInventoryService _inventoryService;
    public PurchaseRequestController(ILogger<PurchaseRequestController> logger,IPurchaseRequestService service,IInventoryService inventoryService)
    {
        _logger = logger;
        _service = service;
        _inventoryService = inventoryService;
    }
    public async Task<IActionResult> Index()
    {
        var purchaseRequests = await _service.GetPurchaseRequests(1,10);
        return View(purchaseRequests);
    }
    public ActionResult Create(CreatePurchaseRequestViewModel purchaseRequest)
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

    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }
        var purchaseRequest = await _service.GetPurchaseRequest(id);
        if (purchaseRequest == null)
        {
            return NotFound();
        }
        return View(purchaseRequest);
    }

    public async Task<IActionResult> Edit(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }
        var purchaseRequest = await _service.GetPurchaseRequest(id);
        if (purchaseRequest == null)
        {
            return NotFound();
        }
        var partNumbers = await _inventoryService.GetPartNumbers(1,10);
        var selectList = new List<SelectListItem>();
        foreach(var item in partNumbers.Items)
        {
            var select = new SelectListItem(){
                Text = item.Id,
                Value = item.Id
            };
            selectList.Add(select);
        }
        selectList.First().Selected = true;
        ViewBag.SelectList = selectList;
        return View(purchaseRequest);
    }
    [HttpPost]
    public async Task<IActionResult> Update([FromBody]UpdatePurchaseRequestViewModel purchaseRequest)
    {
        if (ModelState.IsValid) { 
            await _service.UpdatePurchaseRequest(purchaseRequest);
            return Ok();
        }
        return BadRequest("參數驗證失敗");
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteItem(int id)
    {
        await _service.DeletePurchaseRquestItem(id);
        return Ok("刪除成功");
    }
    public async Task<IActionResult> Delete(string id)
    {
        await _service.DeletePurchaseRequest(id);
        return RedirectToAction("Index");
    }
}