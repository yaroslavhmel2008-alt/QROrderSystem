using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Domain.Entities;
using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Application.Features.DeleteOrderItemCommand;

public class DeleteOrderItemCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
}