using MediatR;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Exceptions;
using QROrderSystem.Application.Interfaces.Services;

namespace QROrderSystem.Application.Features.UpdateOrderCommand;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderDto>
{
    private readonly IOrderService _orderService;
    private readonly ILogger<UpdateOrderCommandHandler> _logger;
    
    public UpdateOrderCommandHandler(IOrderService orderService, ILogger<UpdateOrderCommandHandler> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    public async Task<OrderDto> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to update order with ID: {OrderId} to new location ID: {LocationId}", command.Id, command.LocationId);
        var order = await _orderService.UpdateOrderAsync(command.Id, command.LocationId);
        if (order == null)
        {
            _logger.LogWarning("Order with ID: {OrderId} was not found", command.Id);
            throw new NotFoundException("Order", command.Id);
        }

        _logger.LogInformation("Successfully updated order with ID: {OrderId} to new location ID: {LocationId}", command.Id, command.LocationId);
        return order;
    }
}