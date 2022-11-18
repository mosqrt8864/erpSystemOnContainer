using MediatR;
using Microsoft.AspNetCore.Mvc;
using PurchaseManagement.Application.Commands.CreatePurchaseRequest;
using PurchaseManagement.Application.Queries.GetPurchaseRequest;
using PurchaseManagement.Application.Models;
using PurchaseManagement.Application.Queries.GetPurchaseRequests;
using PurchaseManagement.Application.Commands.UpdatePurchaseRequest;
using PurchaseManagement.Application.Commands.DeletePurchaseRequest;
namespace PurchaseManagement.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PurchaseRequestsController : ControllerBase
{
    private IMediator _mediator;
    private readonly ILogger _logger;
    public PurchaseRequestsController(IMediator mediator,ILogger<PurchaseRequestsController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<bool>> Create([FromBody]CreatePurchaseRequestCommand createPurchaseRequestCommand)
    {
        _logger.LogInformation(
                "----- Sending command: ({@Command})",
                createPurchaseRequestCommand);
        return await _mediator.Send(createPurchaseRequestCommand);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<PurchaseRequestDto>> Get(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }
        return await _mediator.Send(new GetPurchaseRequestQuery(){Id = id});
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<PurchaseRequestsDto>>> GetListAsync([FromQuery] GetPurchaseRequestsQuery query)
    {
        _logger.LogInformation(
                "----- Sending command: ({@Command})",
                query);
        var result = await _mediator.Send(query);
        return result;
    }
    [HttpPatch("{id}")]
    public async Task<ActionResult<bool>> Update(string id ,[FromBody] UpdatePurchaseRequestCommand command)
    {
        command.Id = id;
        _logger.LogInformation(
                "----- Sending command: ({@Command})",
                command);
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(string id)
    {
        return await _mediator.Send(new DeletePurchaseRequestCommand(){Id = id});
    }
}
