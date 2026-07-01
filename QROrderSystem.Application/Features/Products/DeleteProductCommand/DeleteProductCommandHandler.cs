using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Features.Products.DeleteProductCommand;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductService _productService;
    
    public  DeleteProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }
    
    public async Task<bool> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productService.GetProductByIdAsync(command.Id);
        if (product == null)
        {
            throw new Exception($"Product with ID {command.Id} not found.");
        }
        
        return await _productService.DeleteProductAsync(command.Id);
    }
}