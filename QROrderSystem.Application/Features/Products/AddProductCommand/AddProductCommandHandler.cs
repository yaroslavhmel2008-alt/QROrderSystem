using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Features.Products.AddProductCommand;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ProductDto>
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    
    public  AddProductCommandHandler(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }
    
    public async Task<ProductDto> Handle(AddProductCommand command, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetCategoryByIdAsync(command.CategoryId);
        if (category == null)
        {
            throw new Exception($"Category with id {command.CategoryId} does not exist");
        }
                    
        return await _productService.AddProductAsync(command.CategoryId, command.Name, command.Description, command.Price, command.ImageUrl, command.IsAvailable);
    }
}