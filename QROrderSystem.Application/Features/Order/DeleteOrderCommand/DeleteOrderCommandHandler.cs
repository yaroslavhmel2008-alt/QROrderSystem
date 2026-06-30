using MediatR;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Features.DeleteOrderCommand;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly IOrderService _orderService;
    
    public DeleteOrderCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<bool> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        
        var isDeleted = await _orderService.DeleteOrderAsync(command.Id);

        if (!isDeleted)
        {
            throw new Exception($"Order {command.Id} does not exist");
        }
        
        return isDeleted;
    }
}