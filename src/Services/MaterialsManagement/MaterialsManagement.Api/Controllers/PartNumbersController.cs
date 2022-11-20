using MediatR;
using Microsoft.AspNetCore.Mvc;
using MaterialsManagement.Application.Commands.CreatePartNumber;
using MaterialsManagement.Application.Commands.UpdatePartNumber;
using MaterialsManagement.Application.Queries.GetPartNumber;
using MaterialsManagement.Application.Queries.GetPartNumbers;
using MaterialsManagement.Application.Models;
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
            return Ok(result);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PartNumberDto>> Get(string id)
    {
        if (string.IsNullOrEmpty(id)){
            return BadRequest(); 
        }
        var result = await _mediator.Send(new GetPartNumberQuery { Id = id });
        return result;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<PartNumbersDto>>>GetList([FromQuery] GetPartNumbersQuery query)
    {
        _logger.LogInformation(
                "----- Sending command: ({@Command})",
                query);
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<bool>> Update(string id,[FromBody]UpdatePartNumberCommand updatePartNumberCommand)
    {
        updatePartNumberCommand.Id = id;
        _logger.LogInformation(
                "----- Sending command: ({@Command})",
                updatePartNumberCommand);
        return await _mediator.Send(updatePartNumberCommand);
    }
}
