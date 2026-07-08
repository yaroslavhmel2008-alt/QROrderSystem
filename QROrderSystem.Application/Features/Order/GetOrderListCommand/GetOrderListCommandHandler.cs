using MediatR;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces.Services;

namespace QROrderSystem.Application.Features.GetOrderListCommand;

public class GetOrderListCommandHandler : IRequestHandler<GetOrderListCommand, IEnumerable<OrderDto>>
{
    private readonly IOrderService _orderService;
    private readonly ILogger<GetOrderListCommandHandler> _logger;
    
    public GetOrderListCommandHandler(IOrderService orderService, ILogger<GetOrderListCommandHandler> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetOrderListCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching the list of orders");
        var orders = await _orderService.GetOrderListAsync();
        var orderList = orders.ToList();
        _logger.LogInformation("Successfully retrieved {Count} orders ", orderList.Count);
        return orderList;
    }
}  