using AutoMapper;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Exceptions;
using QROrderSystem.Application.Interfaces.Persistence;
using QROrderSystem.Application.Interfaces.Repositories;
using QROrderSystem.Application.Interfaces.Services;
using QROrderSystem.Domain.Entities;

namespace QROrderSystem.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<OrderService> _logger;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper,
        ILogger<OrderService> logger)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<OrderDto> CreateOrderAsync(Guid LocationId, List<OrderItemDto> Items)
    {
        _logger.LogInformation("Attempting to create a new order for LocationId: {LocationId}", LocationId);
        
        var newOrder = new OrderEntity
        {
            Id = Guid.NewGuid(),
            LocationId = LocationId,
            OrderItems = new List<OrderItemEntity>(),
            CreatedAt = DateTime.UtcNow
        };
        
        decimal totalAmount = 0;
        
        foreach (var item in Items)
        {
            var product = await _productRepository.GetProductByIdAsync(item.ProductId);
            if (product == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found during order creation", item.ProductId);
                throw new NotFoundException("Product", item.ProductId);
            }
                

            var newOrderItem = new OrderItemEntity()
            {
                Id = Guid.NewGuid(),
                OrderId = newOrder.Id,
                ProductId = product.Id,
                Quantity = item.Quantity,
                UnitPrice = product.Price,
            };
            
            newOrder.OrderItems.Add(newOrderItem);
            totalAmount += item.Quantity * product.Price;
        }
        newOrder.TotalAmount = totalAmount;
        
        await _orderRepository.AddOrderAsync(newOrder);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Order {OrderId} created successfully with total amount: {TotalAmount}", newOrder.Id, totalAmount);
        return _mapper.Map<OrderDto>(newOrder);
    }

    public async Task<OrderDto> GetOrderByIdAsync(Guid id)
    {
        var order = await _orderRepository.GetOrderByIdAsync(id);
        if (order == null)
        {
            _logger.LogWarning("Order with ID {OrderId} not found during order retrieval", id);
            throw new NotFoundException("Order", id);
        }
        
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<OrderDto> UpdateOrderAsync(Guid id)
    {
        var existingOrder = await _orderRepository.GetOrderByIdAsync(id);
        if (existingOrder == null)
        {
            _logger.LogWarning("Order with ID {OrderId} not found during order update", id);
            throw new NotFoundException("Order", id);
        }
        
        await _orderRepository.UpdateOrderAsync(existingOrder);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Order {OrderId} updated successfully", id);
        return _mapper.Map<OrderDto>(existingOrder);
    }
    
    public async Task<IEnumerable<OrderDto>> GetOrderListAsync()
    {
        _logger.LogInformation("Attempting to retrieve all orders");
        var orders = await _orderRepository.GetOrderListAsync();
        var orderList = orders.ToList();
        _logger.LogInformation("Successfully retrieved {Count} orders", orderList.Count);
        return _mapper.Map<IEnumerable<OrderDto>>(orderList);
    }

    public async Task<bool> DeleteOrderAsync(Guid id)
    {
        var order = await _orderRepository.GetOrderByIdAsync(id);
        if (order == null)
        {
            _logger.LogWarning("Order with ID {OrderId} not found during order deletion", id);
            throw new NotFoundException("Order", id);
        }
        
        var isDeleted = await _orderRepository.DeleteOrderAsync(order);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Order with ID {OrderId} deleted successfully", id);
        return isDeleted;
    }
}

