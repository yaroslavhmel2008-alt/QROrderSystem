using MediatR;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces.Services;

namespace QROrderSystem.Application.Features.Products.GetProductListCommand;

public class GetProductListCommandHandler : IRequestHandler<GetProductListCommand, IEnumerable<ProductDto>>
{
    private readonly IProductService _productService;
    private readonly ILogger<GetProductListCommandHandler> _logger;
    
    public  GetProductListCommandHandler(IProductService productService, ILogger<GetProductListCommandHandler> logger)
    {
        _productService = productService;
        _logger = logger;
    }
    
    public async Task<IEnumerable<ProductDto>> Handle(GetProductListCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to get all products");
        var products = await _productService.GetProductListAsync();
        var productList = products.ToList();
        _logger.LogInformation("Successfully retrieved {Count} products", productList.Count);
        return productList;
    }
}