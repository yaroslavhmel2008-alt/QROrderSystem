using MediatR;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces.Services;

namespace QROrderSystem.Application.Features.CreateCategoryCommand;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CreateCategoryCommandHandler> _logger;
    
    public CreateCategoryCommandHandler(ICategoryService categoryService, ILogger<CreateCategoryCommandHandler> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to create category with Name: {CategoryName} and Description: {CategoryDescription}", command.Name, command.Description);
        var category = await _categoryService.CreateCategoryAsync(command.Name, command?.Description);
        _logger.LogInformation("Category with Name: {CategoryName} successfully created", command.Name);
        return category;
    }
}