using MediatR;
using Microsoft.AspNetCore.Mvc;
using QROrderSystem.Application.Features.CreateLocationCommand;
using QROrderSystem.Application.Features.GetLocationListCommand;
using QROrderSystem.Application.Features.UpdateLocationCommand;

namespace QROrderSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly IMediator _mediator;  
    
    public  LocationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateLocationAsync([FromBody] CreateLocationCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetLocationListAsync()
    {
        var result = await _mediator.Send(new GetLocationListCommand());
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLocationAsync(Guid Id, [FromBody] UpdateLocationCommand command)
    {
        if (Id != command.Id)
        {
            return BadRequest("The ID in the URL does not match the ID in the request body.");
        }
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}