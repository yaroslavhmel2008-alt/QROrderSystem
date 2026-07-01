using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Features.Products.GetProductListCommand;

public class GetProductListCommandHandler : IRequestHandler<GetProductListCommand, IEnumerable<ProductDto>>
{
    private readonly IProductService _productService;
    
    public  GetProductListCommandHandler(IProductService productService)
    {
        _productService = productService;
    }
    
    public async Task<IEnumerable<ProductDto>> Handle(GetProductListCommand command, CancellationToken cancellationToken)
    {
        return await _productService.GetProductListAsync();
    }
}