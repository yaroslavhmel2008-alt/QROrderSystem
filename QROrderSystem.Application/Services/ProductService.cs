using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Services;

public class ProductService : IProductService
{
    public Task<ProductDto> GetProductByIdAsync(Guid productId)
    {
        throw new NotImplementedException();
    }

    public Task<ProductDto> AddProductAsync(Guid categoryId, string name, string? description, decimal price, string? imageUrl,
        bool isAvailable)
    {
        throw new NotImplementedException();
    }
}