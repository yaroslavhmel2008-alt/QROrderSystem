using QROrderSystem.Application.DTOs;

namespace QROrderSystem.Application.Interfaces;

public interface IProductService
{
    Task<ProductDto> GetProductByIdAsync(Guid productId);
}