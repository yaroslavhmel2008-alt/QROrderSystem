using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Domain.Entities;
using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Application.Features.GetOrderListCommand;

public class GetOrderListCommand : IRequest<IEnumerable<OrderDto>>
{
}