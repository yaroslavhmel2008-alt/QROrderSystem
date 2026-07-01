using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Features.Products.UpdateProductCommand;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
{
    private readonly IProductService _productService;
    
    public  UpdateProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }
    
    public async Task<ProductDto> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productService.GetProductByIdAsync(command.Id);
        if (product == null)
        {
            throw new Exception($"Product with ID {command.Id} not found.");
        }
        
        return await _productService.UpdateProductAsync(command.Id, command.CategoryId, command.Name, command.Description, command.Price, command.ImageUrl, command.IsAvailable);
    }
}