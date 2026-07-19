using MediatR;
using Microsoft.AspNetCore.Mvc;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Features.CreateOrderCommand;
using QROrderSystem.Application.Features.GetOrderByIdCommand;
using QROrderSystem.Application.Features.GetOrderListCommand;
using QROrderSystem.Application.Features.UpdateOrderCommand;
using QROrderSystem.Application.Features.UpdateOrderStatusCommand;
using QROrderSystem.Domain.Enums;

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
    public async Task<IActionResult> GetOrderByIdAsync(Guid id)
    {
        var result = await _mediator.Send(new GetOrderByIdCommand { Id = id });
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
    
    [HttpPut("{id}/status")]
    public async Task<ActionResult<OrderDto>> UpdateStatus(Guid id, [FromBody] OrderStatus status)
    {
        var command = new UpdateOrderStatusCommand 
        { 
            Id = id, 
            OrderStatus = status 
        };
        
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}