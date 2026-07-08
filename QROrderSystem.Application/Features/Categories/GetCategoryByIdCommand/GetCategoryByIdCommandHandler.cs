using MediatR;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Exceptions;
using QROrderSystem.Application.Interfaces.Services;

namespace QROrderSystem.Application.Features.GetCategoryByIdCommand;

public class GetCategoryByIdCommandHandler : IRequestHandler<GetCategoryByIdCommand, CategoryDto>
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<GetCategoryByIdCommandHandler> _logger;
    
    public GetCategoryByIdCommandHandler(ICategoryService categoryService, ILogger<GetCategoryByIdCommandHandler> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    public async Task<CategoryDto> Handle(GetCategoryByIdCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching category with ID: {CategoryId}", command.Id);
        var category = await _categoryService.GetCategoryByIdAsync(command.Id);
        if (category == null)
        {
            _logger.LogWarning("Category with ID {CategoryId} was not found", command.Id);
            throw new NotFoundException($"Category", command.Id);
        }
        return category;
    }
}