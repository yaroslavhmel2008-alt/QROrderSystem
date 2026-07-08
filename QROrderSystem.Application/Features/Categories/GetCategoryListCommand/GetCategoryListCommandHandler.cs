using MediatR;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces.Services;

namespace QROrderSystem.Application.Features.GetCategoryListCommand;

public class GetCategoryListCommandHandler : IRequestHandler<GetCategoryListCommand, IEnumerable<CategoryDto>>
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<GetCategoryListCommandHandler> _logger;
    
    public GetCategoryListCommandHandler(ICategoryService categoryService, ILogger<GetCategoryListCommandHandler> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    public async Task<IEnumerable<CategoryDto>> Handle(GetCategoryListCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching the list of categories");
        var categories = await _categoryService.GetCategoryListAsync();
        var categoryList = categories.ToList();
        
        _logger.LogInformation("Successfully retrieved {Count} categories", categoryList.Count);
        return categoryList;
    }
}