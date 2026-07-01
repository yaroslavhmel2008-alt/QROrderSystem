using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Services;

public class OrderItemService : IOrderItemService
{
    public Task<OrderItemDto> AddOrderItemToOrderAsync(Guid OrderId, Guid ProductId, int Quantity)
    {
        throw new NotImplementedException();
    }

    public Task<OrderItemDto> UpdateOrderItemAsync(Guid Id, Guid OrderId, int Quantity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteOrderItemAsync(Guid Id, Guid ProductId)
    {
        throw new NotImplementedException();
    }
}