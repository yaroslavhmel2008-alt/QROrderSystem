using QROrderSystem.Domain.Entities;

namespace QROrderSystem.Application.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<OrderEntity>> GetOrderListAsync();
    Task<OrderEntity> AddOrderAsync(OrderEntity orderEntity);
    Task<OrderEntity?> GetOrderByIdAsync(Guid id);
    Task<OrderEntity?> UpdateOrderAsync(OrderEntity orderEntity);
    Task<bool> DeleteOrderAsync(OrderEntity orderEntity);
}