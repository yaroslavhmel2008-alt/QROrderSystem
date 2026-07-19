using MediatR;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Exceptions;
using QROrderSystem.Application.Interfaces.Services;

namespace QROrderSystem.Application.Features.UpdateOrderStatusCommand;

public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, OrderDto>
{
    private readonly IOrderService _orderService;
    private readonly ILogger<UpdateOrderStatusCommandHandler> _logger;
    
    public UpdateOrderStatusCommandHandler(IOrderService orderService, ILogger<UpdateOrderStatusCommandHandler> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    public async Task<OrderDto> Handle(UpdateOrderStatusCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to update order status with ID: {OrderId} to: {Status}", command.Id, command.OrderStatus);
        var order = await _orderService.UpdateOrderStatusAsync(command.Id, command.OrderStatus);
        if (order == null)
        {
            _logger.LogWarning("Order with ID: {OrderId} was not found", command.Id);
            throw new NotFoundException("Order", command.Id);
        }

        _logger.LogInformation("Successfully updated order status with ID: {OrderId} to: {Status}", command.Id, command.OrderStatus);
        return order;
    }
}