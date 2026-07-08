using MediatR;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Exceptions;
using QROrderSystem.Application.Interfaces.Services;

namespace QROrderSystem.Application.Features.CreateOrderCommand;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderService _orderService;
    private readonly ILocationService _locationService;
    private readonly ILogger<CreateOrderCommandHandler> _logger;


    public CreateOrderCommandHandler(IOrderService orderService, ILocationService locationService, ILogger<CreateOrderCommandHandler> logger)
    {
        _orderService = orderService;
        _locationService = locationService;
        _logger = logger;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating order for Location ID: {LocationId} with {ItemCount} items", command.LocationId, command.Items.Count);
    
        var location = await _locationService.GetLocationByIdAsync(command.LocationId);
        if (location == null)
        {
            _logger.LogWarning("Location with ID: {LocationId} was not found", command.LocationId);
            throw new NotFoundException("Location", command.LocationId);
        }
        
        var order = await _orderService.CreateOrderAsync(command.LocationId, command.Items);
        
        _logger.LogInformation("Successfully created order with ID: {OrderId} for Location ID: {LocationId}", order.Id, command.LocationId);
    
        return order;
    }
}