using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using QROrderSystem.Application.Interfaces.Repositories;
using QROrderSystem.Domain.Entities;
using QROrderSystem.Infrastructure.Persistence;


namespace QROrderSystem.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;
    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<OrderEntity>> GetOrderListAsync()
    {
        return await _context.Orders
            .Include(o => o.OrderItems).ThenInclude(oi => oi.ProductEntity).Include(o => o.LocationEntity).ToListAsync();
    }

    public async Task<OrderEntity> AddOrderAsync(OrderEntity orderEntity)
    {
        await _context.Orders.AddAsync(orderEntity);
        return orderEntity;
    }

    public async Task<OrderEntity?> GetOrderByIdAsync(Guid id)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.ProductEntity)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public Task<OrderEntity?> UpdateOrderAsync(OrderEntity orderEntity)
    {
        _context.Orders.Update(orderEntity);
        return Task.FromResult(orderEntity);
    }

    public Task<bool> DeleteOrderAsync(OrderEntity orderEntity)
    {
        _context.Orders.Remove(orderEntity);
        return Task.FromResult(true);
    }
}