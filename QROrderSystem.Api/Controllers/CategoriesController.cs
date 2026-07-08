using MediatR;
using Microsoft.AspNetCore.Mvc;
using QROrderSystem.Application.Features.CreateCategoryCommand;
using QROrderSystem.Application.Features.DeleteCategoryCommand;
using QROrderSystem.Application.Features.GetCategoryByIdCommand;
using QROrderSystem.Application.Features.GetCategoryListCommand;
using QROrderSystem.Application.Features.UpdateCategoryCommand;

namespace QROrderSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public  CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryByIdAsync(Guid id)
    {
        var result = await _mediator.Send(new GetCategoryByIdCommand {Id = id});

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoriesAsync()
    {
        var result = await _mediator.Send(new GetCategoryListCommand());
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoryAsync(Guid id)
    {
        var result = await _mediator.Send(new DeleteCategoryCommand {Id = id});
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategoryAsync(Guid id, [FromBody] UpdateCategoryCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("The ID in the URL does not match the ID in the request body.");
        }
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    
}