using QROrderSystem.Application.DTOs;
using QROrderSystem.Domain.Entities;

namespace QROrderSystem.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<ProductEntity?> GetProductByNameAsync(string name);
    Task<ProductEntity> AddProductAsync(ProductEntity productEntity);
    Task<ProductEntity> UpdateProductAsync(ProductEntity productEntity);
    Task<ProductEntity?> GetProductByIdAsync(Guid Id);
    Task<IEnumerable<ProductEntity>> GetProductListAsync();
    Task<IEnumerable<ProductEntity>> GetProductsByCategoryIdAsync(Guid categoryId);
    Task<bool> DeleteProductByIdAsync(ProductEntity productEntity);
}