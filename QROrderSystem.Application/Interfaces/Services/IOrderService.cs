using QROrderSystem.Application.DTOs;
using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Application.Interfaces.Services;

public interface IOrderService
{
    Task<OrderDto> CreateOrderAsync(Guid LocationId,  List<OrderItemDto> Items);
    Task<OrderDto> GetOrderByIdAsync(Guid id);
    Task<OrderDto> UpdateOrderAsync(Guid id);
    Task<IEnumerable<OrderDto>> GetOrderListAsync();
    Task<bool> DeleteOrderAsync(Guid id);
    Task<OrderDto> UpdateOrderStatusAsync(Guid id, OrderStatus status);
}