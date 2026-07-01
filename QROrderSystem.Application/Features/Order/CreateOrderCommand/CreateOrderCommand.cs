using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Domain.Entities;
using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Application.Features.CreateOrderCommand;

public class CreateOrderCommand : IRequest<OrderDto>
{
    public Guid LocationId { get; set; }
}