using QROrderSystem.Application.DTOs;

namespace QROrderSystem.Application.Interfaces;

public interface IProductService
{
    Task<ProductDto> GetProductByIdAsync(Guid productId);
    Task<ProductDto> AddProductAsync(Guid categoryId, string name, string? description, decimal price, string? imageUrl, bool isAvailable);
}