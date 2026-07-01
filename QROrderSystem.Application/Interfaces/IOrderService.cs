using QROrderSystem.Application.DTOs;

namespace QROrderSystem.Application.Interfaces;

public interface IOrderService
{
    Task<OrderDto> CreateOrderAsync(Guid LocationId);
    Task<OrderDto> GetOrderByIdAsync(Guid id);
    Task<OrderDto> UpdateOrderAsync(Guid id, Guid LocationId);
    Task<IEnumerable<OrderDto>> GetOrderListAsync();
    Task<bool> DeleteOrderAsync(Guid id);
}