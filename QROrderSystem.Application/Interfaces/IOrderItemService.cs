using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Features.AddOrderItemToOrderCommand;

namespace QROrderSystem.Application.Interfaces;

public interface IOrderItemService
{
    Task<OrderItemDto> AddOrderItemToOrderAsync(Guid OrderId, Guid ProductId, int Quantity);
    Task<OrderItemDto> UpdateOrderItemAsync(Guid Id, Guid OrderId, int Quantity);
}