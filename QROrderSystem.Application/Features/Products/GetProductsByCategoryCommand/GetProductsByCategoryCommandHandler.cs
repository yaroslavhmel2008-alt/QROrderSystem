using MediatR;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Exceptions;
using QROrderSystem.Application.Interfaces.Services;

namespace QROrderSystem.Application.Features.Products.GetProductsByCategoryCommand;

public class GetProductsByCategoryCommandHandler : IRequestHandler<GetProductsByCategoryCommand, IEnumerable<ProductDto>>
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly ILogger<GetProductsByCategoryCommandHandler> _logger;
    
    public  GetProductsByCategoryCommandHandler(IProductService productService,  ICategoryService categoryService, ILogger<GetProductsByCategoryCommandHandler> logger)
    {
        _productService = productService;
        _categoryService = categoryService;
        _logger = logger;
    }
    
    public async Task<IEnumerable<ProductDto>> Handle(GetProductsByCategoryCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to get products for category ID: {CategoryId}", command.CategoryId);
    
        var category = await _categoryService.GetCategoryByIdAsync(command.CategoryId);
        if(category == null)
        {
            _logger.LogWarning("Category with ID {CategoryId} does not exist", command.CategoryId);
            throw new NotFoundException("Category", command.CategoryId);
        }
        
        var products = await _productService.GetProductsByCategoryAsync(command.CategoryId);
        var productList = products.ToList(); 
    
        _logger.LogInformation("Successfully retrieved {Count} products for category ID: {CategoryId}", productList.Count, command.CategoryId);
    
        return productList;
    }
}