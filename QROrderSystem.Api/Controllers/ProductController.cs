using MediatR;
using Microsoft.AspNetCore.Mvc;
using QROrderSystem.Application.Features.DeleteOrderCommand;
using QROrderSystem.Application.Features.GetOrderByIdCommand;
using QROrderSystem.Application.Features.GetOrderListCommand;
using QROrderSystem.Application.Features.Products.AddProductCommand;
using QROrderSystem.Application.Features.Products.DeleteProductCommand;
using QROrderSystem.Application.Features.Products.GetProductListCommand;
using QROrderSystem.Application.Features.Products.GetProductsByCategoryCommand;
using QROrderSystem.Application.Features.Products.UpdateProductCommand;
using QROrderSystem.Application.Features.UpdateOrderCommand;


namespace QROrderSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductAsync([FromBody] AddProductCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetProductListAsync()
    {
        var result = await _mediator.Send(new GetProductListCommand());
        return Ok(result);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateProductAsync(Guid Id, [FromBody] UpdateProductCommand command)
    {
        if (Id != command.Id)
        {
            return BadRequest("The ID in the URL does not match the ID in the request body.");
        }
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductAsync(Guid id)
    {
        var result = await _mediator.Send(new DeleteProductCommand { Id = id });
        return Ok(result);
    }

    [HttpGet("category/{CategoryId}")]
    public async Task<IActionResult> GetProductByCategoryIdAsync(Guid CategoryId)
    {
        var result = await _mediator.Send(new GetProductsByCategoryCommand { CategoryId = CategoryId });
        return Ok(result);
    }
}
