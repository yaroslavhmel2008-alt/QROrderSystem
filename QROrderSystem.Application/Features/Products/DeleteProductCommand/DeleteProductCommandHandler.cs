using MediatR;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Exceptions;
using QROrderSystem.Application.Interfaces.Services;

namespace QROrderSystem.Application.Features.Products.DeleteProductCommand;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductService _productService;
    private readonly ILogger<DeleteProductCommandHandler> _logger;
    
    public  DeleteProductCommandHandler(IProductService productService, ILogger<DeleteProductCommandHandler> logger)
    {
        _productService = productService;
        _logger = logger;
    }
    
    public async Task<bool> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to delete product with ID: {ProductId}", command.Id);
        var product = await _productService.GetProductByIdAsync(command.Id);
        if (product == null)
        {
            _logger.LogWarning("Product with ID: {ProductId} was not found", command.Id);
            throw new NotFoundException("Product", command.Id);
        }
        _logger.LogInformation("Successfully deleted product with ID: {ProductId}", command.Id);
        
        return await _productService.DeleteProductAsync(command.Id);
    }
}