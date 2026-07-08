using MediatR;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Exceptions;
using QROrderSystem.Application.Interfaces.Services;

namespace QROrderSystem.Application.Features.Products.AddProductCommand;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ProductDto>
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly ILogger<AddProductCommandHandler> _logger;
    
    public  AddProductCommandHandler(IProductService productService, ICategoryService categoryService, ILogger<AddProductCommandHandler> logger)
    {
        _productService = productService;
        _categoryService = categoryService;
        _logger = logger;
    }
    
    public async Task<ProductDto> Handle(AddProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to add product '{Name}' to Category {CategoryId}", command.Name, command.CategoryId);
        var category = await _categoryService.GetCategoryByIdAsync(command.CategoryId);
        if (category == null)
        {
            _logger.LogWarning("Category with ID: {CategoryId} was not found", command.CategoryId);
            throw new NotFoundException("Category", command.CategoryId);
        }
        var product = await _productService.AddProductAsync(command.CategoryId, command.Name, command.Description, command.Price, command.ImageUrl, command.IsAvailable);
        _logger.LogInformation("Successfully added product with Name: {ProductName}, Description: {ProductDescription}, Price: {ProductPrice}, ImageUrl: {ProductImageUrl}, IsAvailable: {ProductIsAvailable} to CategoryId: {CategoryId}", command.Name, command.Description, command.Price, command.ImageUrl, command.IsAvailable, command.CategoryId);
        return product;
    }
}