using System.Globalization;
using MediatR;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.Exceptions;
using QROrderSystem.Application.Interfaces.Services;

namespace QROrderSystem.Application.Features.DeleteCategoryCommand;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<DeleteCategoryCommandHandler> _logger;
    
    public DeleteCategoryCommandHandler(ICategoryService categoryService,  ILogger<DeleteCategoryCommandHandler> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to delete category with ID: {CategoryId}", command.Id);
        
        var isDeleted = await _categoryService.DeleteCategoryAsync(command.Id);
        if (!isDeleted)
        {
            _logger.LogWarning("Category with ID: {CategoryId} not found.", command.Id);
            throw new NotFoundException($"Category", command.Id);
        }
        
        _logger.LogInformation("Category with ID {CategoryId} successfully deleted", command.Id);
        return isDeleted;
    }
}