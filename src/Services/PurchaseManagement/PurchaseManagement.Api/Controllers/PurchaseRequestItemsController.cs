using MediatR;
using Microsoft.AspNetCore.Mvc;
using PurchaseManagement.Application.Commands.DeletePurchaseRequestItem;
[ApiController]
[Route("/api/v1/[controller]")]
public class PurchaseRequestItemsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PurchaseRequestItemsController> _logger;
    public PurchaseRequestItemsController(ILogger<PurchaseRequestItemsController> logger,IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger;
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        try
        {
            var result = await _mediator.Send(new DeletePurchaseRequestItemCommand(){Id = id});
            return Ok(result);
        }
        catch(Exception ex)
        {
            _logger.LogError("ErrMsg: {@string} , StatusCode: {code}",ex.Message.ToString(),500);
            return StatusCode(500);
        }
    }
}