using MediatR;
using Microsoft.AspNetCore.Mvc;
using PurchaseManagement.Application.Commands.CreatePurchaseRequest;
using PurchaseManagement.Application.Queries.GetPurchaseRequest;
using PurchaseManagement.Application.Models;
using PurchaseManagement.Application.Queries.GetPurchaseRequests;
using PurchaseManagement.Application.Commands.UpdatePurchaseRequest;
using PurchaseManagement.Application.Commands.DeletePurchaseRequest;
using PurchaseManagement.Api.Errors;
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

        try 
        {
            _logger.LogInformation(
                "----- Sending command: ({@Command})",
                createPurchaseRequestCommand);
            var result = await _mediator.Send(createPurchaseRequestCommand);
            return Ok(result);
        }
        catch(Exception ex)
        {
            switch (ex)
            {
                case System.ArgumentException:
                    var errorType = new PurchaseRequestErrorFeature(){
                        PurchaseRequestError = PurchaseRequestErrorType.CreateExistKeyError
                    }; 
                    HttpContext.Features.Set(errorType);
                    _logger.LogError("ErrMsg: {@string} , StatusCode: {code}.",ex.Message.ToString(),409);
                    return Conflict();
                default :
                    _logger.LogError("ErrMsg: {@string} , StatusCode: {code}",ex.Message.ToString(),500);
                    return StatusCode(500);
            }
            
        }
        
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<PurchaseRequestDto>> Get(string id)
    {
        try
        {
            var result = await _mediator.Send(new GetPurchaseRequestQuery(){Id = id});
            if (result ==null)
            {
                var errorType = new PurchaseRequestErrorFeature()
                {
                    PurchaseRequestError= PurchaseRequestErrorType.GetNotExistKeyError
                };
                HttpContext.Features.Set(errorType);
                return BadRequest();
            }
            return Ok(result);
        }
        catch(Exception ex)
        {
            _logger.LogError("ErrMsg: {@string} , StatusCode: {code}",ex.Message.ToString(),500);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<PurchaseRequestsDto>>> GetListAsync([FromQuery] GetPurchaseRequestsQuery query)
    {
        try
        {
            _logger.LogInformation(
                "----- Sending command: ({@Command})",
                query);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch(Exception ex)
        {
            _logger.LogError("ErrMsg: {@string} , StatusCode: {code}",ex.Message.ToString(),500);
            return StatusCode(500);
        }
        
    }
    [HttpPatch("{id}")]
    public async Task<ActionResult<bool>> Update(string id ,[FromBody] UpdatePurchaseRequestCommand command)
    {
        try
        {
            command.Id = id;
            _logger.LogInformation(
                    "----- Sending command: ({@Command})",
                    command);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch(Exception ex)
        {
            _logger.LogError("ErrMsg: {@string} , StatusCode: {code}",ex.Message.ToString(),500);
            return StatusCode(500);
        }
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(string id)
    {
        try
        {
            var result = await _mediator.Send(new DeletePurchaseRequestCommand(){Id = id});
            return Ok(result);
        }
        catch(Exception ex)
        {
            _logger.LogError("ErrMsg: {@string} , StatusCode: {code}",ex.Message.ToString(),500);
            return StatusCode(500);
        }
    }
}
