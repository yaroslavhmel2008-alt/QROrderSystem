using MediatR;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.Exceptions;
using QROrderSystem.Application.Interfaces.Services;

namespace QROrderSystem.Application.Features.DeleteOrderCommand;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly IOrderService _orderService;
    private readonly ILogger<DeleteOrderCommandHandler> _logger;
    
    public DeleteOrderCommandHandler(IOrderService orderService, ILogger<DeleteOrderCommandHandler> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to delete order with ID: {OrderId}", command.Id);
    
        var isDeleted = await _orderService.DeleteOrderAsync(command.Id);

        if (!isDeleted)
        {
            _logger.LogWarning("Failed to delete order with ID: {OrderId}. Not found.", command.Id);
            throw new NotFoundException("Order", command.Id);
        }
    
        _logger.LogInformation("Successfully deleted order with ID: {OrderId}", command.Id);
    
        return isDeleted;
    }
}