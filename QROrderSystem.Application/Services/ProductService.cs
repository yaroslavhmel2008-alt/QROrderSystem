using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Services;

public class ProductService : IProductService
{
    public Task<ProductDto> GetProductByIdAsync(Guid productId)
    {
        throw new NotImplementedException();
    }
}