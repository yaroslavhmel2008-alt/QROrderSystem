using MediatR;
using Microsoft.AspNetCore.Mvc;
using QROrderSystem.Application.Features.CreateOrderCommand;
using QROrderSystem.Application.Features.GetOrderByIdCommand;
using QROrderSystem.Application.Features.GetOrderListCommand;
using QROrderSystem.Application.Features.UpdateOrderCommand;

namespace QROrderSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetOrderListAsync()
    {
        var result = await _mediator.Send(new GetOrderListCommand());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderByIdAsync(Guid Id)
    {
        var result = await _mediator.Send(new GetOrderByIdCommand { Id = Id });
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateOrderAsync(Guid Id, [FromBody] UpdateOrderCommand command)
    {
        if (Id != command.Id)
        {
            return BadRequest("The ID in the URL does not match the ID in the request body.");
        }
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}