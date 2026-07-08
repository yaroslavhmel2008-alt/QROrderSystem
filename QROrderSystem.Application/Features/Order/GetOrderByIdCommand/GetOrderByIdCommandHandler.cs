using MediatR;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Exceptions;
using QROrderSystem.Application.Interfaces.Services;

namespace QROrderSystem.Application.Features.GetOrderByIdCommand;

public class GetOrderByIdCommandHandler : IRequestHandler<GetOrderByIdCommand, OrderDto>
{
    private readonly IOrderService _orderService;
    private readonly ILogger<GetOrderByIdCommandHandler> _logger;
    
    public GetOrderByIdCommandHandler(IOrderService orderService, ILogger<GetOrderByIdCommandHandler> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    public async Task<OrderDto> Handle(GetOrderByIdCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to get order with ID: {OrderId}", command.Id);
        var order = await _orderService.GetOrderByIdAsync(command.Id);
        if (order == null)
        {
            _logger.LogWarning("Failed to find order with ID: {OrderId}", command.Id);
            throw new NotFoundException("Order", command.Id);
        }
    
        _logger.LogInformation("Successfully retrieved order with ID: {OrderId}", command.Id);
        return order;
    }
}