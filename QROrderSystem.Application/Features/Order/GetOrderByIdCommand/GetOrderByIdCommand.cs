using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Domain.Entities;
using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Application.Features.GetOrderByIdCommand;

public class GetOrderByIdCommand : IRequest<OrderDto>
{
    public Guid Id { get; set; }
} 