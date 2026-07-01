using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Services;

public class OrderService : IOrderService
{
    public Task<OrderDto> CreateOrderAsync(Guid LocationId)
    {
        throw new NotImplementedException();
    }

    public Task<OrderDto> GetOrderByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<OrderDto> UpdateOrderAsync(Guid id, Guid LocationId)
    {
        throw new NotImplementedException();
    }
    
    public Task<IEnumerable<OrderDto>> GetOrderListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteOrderAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}