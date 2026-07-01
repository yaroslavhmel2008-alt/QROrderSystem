using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Features.CreateOrderCommand;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderService _orderService;
    private readonly ILocationService _locationService;


    public CreateOrderCommandHandler(IOrderService orderService, ILocationService locationService)
    {
        _orderService = orderService;
        _locationService = locationService;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var location = await _locationService.GetLocationByIdAsync(command.LocationId);
        if (location == null)
        {
            throw new Exception($"Локацію з ID {command.LocationId} не знайдено");
        }

        return await _orderService.CreateOrderAsync(command.LocationId);
    }
}