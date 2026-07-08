using QROrderSystem.Application.Interfaces.Persistence;
using QROrderSystem.Application.Interfaces.Repositories;
using QROrderSystem.Infrastructure.Repositories;

namespace QROrderSystem.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    
    public IOrderRepository Orders { get; }
    public IProductRepository Products { get; }
    public IOrderItemRepository OrderItems { get; }
    public ILocationRepository Locations { get; }
    public ICategoryRepository Categories { get; }

    public UnitOfWork(ApplicationDbContext context, IOrderRepository orders, IProductRepository products, IOrderItemRepository orderItems, ILocationRepository locations, ICategoryRepository categories)
    {
        _context = context;
        Orders = orders;
        Products = products;
        OrderItems = orderItems;
        Locations = locations;
        Categories = categories;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await _context.SaveChangesAsync(ct);
    }

    public void Dispose() => _context.Dispose();
}