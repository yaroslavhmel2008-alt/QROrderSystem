using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Features.UpdateOrderCommand;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderDto>
{
    private readonly IOrderService _orderService;
    
    public UpdateOrderCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<OrderDto> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _orderService.UpdateOrderAsync(command.Id, command.LocationId);
        if (order == null)
        {
            throw new Exception($"Order {command.Id} does not exist");
        }

        return order;
    }
}