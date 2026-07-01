using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Features.GetOrderByIdCommand;

public class GetOrderByIdCommandHandler : IRequestHandler<GetOrderByIdCommand, OrderDto>
{
    private readonly IOrderService _orderService;
    
    public GetOrderByIdCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<OrderDto> Handle(GetOrderByIdCommand command, CancellationToken cancellationToken)
    {
        
        var order = await _orderService.GetOrderByIdAsync(command.Id);
        if (order == null)
        {
            throw new Exception($"Order {command.Id} does not exist");
        }

        return order;
    }
}