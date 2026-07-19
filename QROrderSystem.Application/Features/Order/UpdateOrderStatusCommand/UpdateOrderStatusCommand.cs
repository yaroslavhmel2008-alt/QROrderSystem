using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Domain.Entities;
using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Application.Features.UpdateOrderStatusCommand;

public class UpdateOrderStatusCommand : IRequest<OrderDto>
{
    public Guid Id { get; set; }
    public OrderStatus OrderStatus { get; set; }
}