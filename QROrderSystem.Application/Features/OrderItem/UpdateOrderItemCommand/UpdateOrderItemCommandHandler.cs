using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Features.UpdateOrderItemCommand;

public class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand, OrderItemDto>
{
    private readonly IOrderItemService _orderItemService;
    private readonly IOrderService _orderService;
    
    public UpdateOrderItemCommandHandler(IOrderItemService orderItemService,  IOrderService orderService)
    {
        _orderItemService = orderItemService;
        _orderService = orderService;
    }

    public async Task<OrderItemDto> Handle(UpdateOrderItemCommand command, CancellationToken cancellationToken)
    {
        var order = await _orderService.GetOrderByIdAsync(command.OrderId);
        if (order == null)
        {
            throw new Exception($"Order {command.OrderId} does not exist");
        }

        if (!order.OrderItems.Any(item => item.Id == command.Id))
        {
            throw new Exception($"Item {command.Id} not found in order {command.OrderId}");
        }
        
        return await _orderItemService.UpdateOrderItemAsync(command.Id, command.OrderId, command.Quantity);
    }
}