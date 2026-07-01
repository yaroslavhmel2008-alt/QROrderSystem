using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Features.DeleteOrderItemCommand;

public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand, bool>
{
    private readonly IOrderItemService _orderItemService;
    private readonly IOrderService _orderService;
    
    public DeleteOrderItemCommandHandler(IOrderItemService orderItemService,  IOrderService orderService)
    {
        _orderItemService = orderItemService;
        _orderService = orderService;
    }

    public async Task<bool> Handle(DeleteOrderItemCommand command, CancellationToken cancellationToken)
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
        
        var isDeleted = await _orderItemService.DeleteOrderItemAsync(command.Id, command.OrderId);

        if (!isDeleted)
        {
            throw new Exception($"Failed to delete item {command.Id} from order {command.OrderId}");
        }
        
        return isDeleted;
    }
}