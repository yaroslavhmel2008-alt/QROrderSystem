using MediatR;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Exceptions;
using QROrderSystem.Application.Interfaces.Services;

namespace QROrderSystem.Application.Features.Products.UpdateProductCommand;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
{
    private readonly IProductService _productService;
    private readonly ILogger<UpdateProductCommandHandler> _logger;
    
    public  UpdateProductCommandHandler(IProductService productService, ILogger<UpdateProductCommandHandler> logger)
    {
        _productService = productService;
        _logger = logger;
    }
    
    public async Task<ProductDto> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating product {ProductId}", command.Id);
        var existingProduct = await _productService.GetProductByIdAsync(command.Id);
        if (existingProduct == null)
        {
            _logger.LogWarning("Product {ProductId} doesn't exist", command.Id);
            throw new NotFoundException("Product", command.Id);
        }
        _logger.LogInformation("Updating product {ProductId}", command.Id);
        var updatedProduct = await _productService.UpdateProductAsync(command.Id, command.CategoryId, command.Name, command.Description, command.Price, command.ImageUrl, command.IsAvailable);
        _logger.LogInformation("Product {ProductId} updated successfully", command.Id);
        return updatedProduct;
    }
}