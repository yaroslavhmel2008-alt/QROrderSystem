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
        return await _context.Orders.ToListAsync();
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
            .FirstOrDefaultAsync(o => o.Id == id);
    }
}