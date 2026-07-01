using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Features.Products.GetProductsByCategoryCommand;

public class GetProductsByCategoryCommandHandler : IRequestHandler<GetProductsByCategoryCommand, IEnumerable<ProductDto>>
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    
    public  GetProductsByCategoryCommandHandler(IProductService productService,  ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }
    
    public async Task<IEnumerable<ProductDto>> Handle(GetProductsByCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetCategoryByIdAsync(command.CategoryId);
        if(category == null)
        {
            throw new Exception($"Category with ID {command.CategoryId} not found.");
        }
        return await _productService.GetProductsByCategoryAsync(command.CategoryId);
    }
}