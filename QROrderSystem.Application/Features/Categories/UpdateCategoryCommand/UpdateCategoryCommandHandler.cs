using MediatR;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Exceptions;
using QROrderSystem.Application.Interfaces.Services;

namespace QROrderSystem.Application.Features.UpdateCategoryCommand;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<UpdateCategoryCommandHandler> _logger;
    
    public UpdateCategoryCommandHandler(ICategoryService categoryService, ILogger<UpdateCategoryCommandHandler> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    public async Task<CategoryDto> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating the category");
        var category = await _categoryService.UpdateCategoryAsync(command.Id, command.Name, command?.Description);
        if (category == null)
        {
            _logger.LogWarning("Category with ID {CategoryId} was not found", command.Id);
            throw new NotFoundException("Category", command.Id);
        }
        _logger.LogInformation("Category updated");
        return category;
    }
}