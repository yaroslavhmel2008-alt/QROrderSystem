using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Features.GetOrderListCommand;

public class GetOrderListCommandHandler : IRequestHandler<GetOrderListCommand, IEnumerable<OrderDto>>
{
    private readonly IOrderService _orderService;
    
    public GetOrderListCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetOrderListCommand command, CancellationToken cancellationToken)
    {
        
        return await _orderService.GetOrderListAsync();
    }
}  