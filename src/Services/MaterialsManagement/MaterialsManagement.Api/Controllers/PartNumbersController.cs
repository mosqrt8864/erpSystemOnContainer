using MediatR;
using Microsoft.AspNetCore.Mvc;
using MaterialsManagement.Application.Commands.CreatePartNumber;
using MaterialsManagement.Application.Commands.UpdatePartNumber;
using MaterialsManagement.Application.Queries.GetPartNumber;
using MaterialsManagement.Application.Queries.GetPartNumbers;
using MaterialsManagement.Application.Models;
using Microsoft.AspNetCore.Diagnostics;
using MaterialsManagement.Api.Errors;
using System.Net;
namespace MaterialsManagement.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PartNumbersController : ControllerBase
{
    private IMediator _mediator;
    private readonly ILogger _logger;
    public PartNumbersController(IMediator mediator,ILogger<PartNumbersController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<bool>> Create([FromBody]CreatePartNumberCommand createPartNumberCommand)
    {
        try
        {
            _logger.LogInformation(
                    "----- Sending command: ({@Command})",
                    createPartNumberCommand);
            var result = await _mediator.Send(createPartNumberCommand);
            return result;
        }
        catch(Exception ex)
        {
            switch (ex)
            {
                case System.ArgumentException:
                    var errorType = new PartNumberErrorFeature()
                    {
                        PartNumberError = PartNumberErrorType.SameKeyError
                    };
                    HttpContext.Features.Set(errorType);
                    _logger.LogError("ErrMsg: {@string} , StatusCode: {code}.",ex.Message.ToString(),409);
                    return Conflict();
                default:
                    _logger.LogError("ErrMsg: {@string} , StatusCode: {code}.",ex.Message.ToString(),500);
                    return StatusCode(500);
            }
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PartNumberDto>> Get(string id)
    {
        try
        {
            var result = await _mediator.Send(new GetPartNumberQuery { Id = id });
            if(result==null)
            {
                var errorType = new PartNumberErrorFeature()
                {
                    PartNumberError = PartNumberErrorType.NotExistKeyError
                };
                HttpContext.Features.Set(errorType);
                return BadRequest();
            }
            return result;
        }
        catch(Exception ex)
        {
            _logger.LogError("ErrMsg: {@string} , StatusCode: {code}.",ex.Message.ToString(),500);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<PartNumbersDto>>>GetList([FromQuery] GetPartNumbersQuery query)
    {
        try
        {
            _logger.LogInformation(
                    "----- Sending command: ({@Command})",
                    query);
            var result = await _mediator.Send(query);
            return result;
        }
        catch(Exception ex)
        {
            _logger.LogError("ErrMsg: {@string} , StatusCode: {code}.",ex.Message.ToString(),500);
            return StatusCode(500);
        }
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<bool>> Update(string id,[FromBody]UpdatePartNumberCommand updatePartNumberCommand)
    {
        try
        {
            updatePartNumberCommand.Id = id;
            _logger.LogInformation(
                    "----- Sending command: ({@Command})",
                    updatePartNumberCommand);
            return await _mediator.Send(updatePartNumberCommand);
        }
        catch(Exception ex)
        {
            _logger.LogError("ErrMsg: {@string} , StatusCode: {code}.",ex.Message.ToString(),500);
            return StatusCode(500);
        }
    }
    
}
