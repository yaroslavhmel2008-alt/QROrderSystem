using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Features.AddOrderItemToOrderCommand;

public class AddOrderItemToOrderCommandHandler : IRequestHandler<AddOrderItemToOrderCommand, OrderItemDto>
{
    private readonly IOrderItemService _orderItemService;
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    
    public AddOrderItemToOrderCommandHandler(IOrderItemService orderItemService,  IOrderService orderService, IProductService productService)
    {
        _orderItemService = orderItemService;
        _orderService = orderService;
        _productService = productService;
    }

    public async Task<OrderItemDto> Handle(AddOrderItemToOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _orderService.GetOrderByIdAsync(command.OrderId);
        if (order == null)
        {
            throw new Exception($"Order {command.OrderId} does not exist");
        }
        
        var product = await _productService.GetProductByIdAsync(command.ProductId);

        if (product == null)
        {
            throw new Exception($"Product {command.ProductId} does not exist");
        }
        
        return await _orderItemService.AddOrderItemToOrderAsync(command.OrderId, command.ProductId, command.Quantity);
    }
}