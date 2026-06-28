using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Domain.Entities;
using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Application.Features.UpdateOrderCommand;

public class UpdateOrderCommand : IRequest<OrderDto>
{
    public Guid Id { get; set; }
    public Guid LocationId { get; set; }
}