using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Domain.Entities;
using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Application.Features.UpdateOrderItemCommand;

public class UpdateOrderItemCommand : IRequest<OrderItemDto>
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public int Quantity { get; set; }
}