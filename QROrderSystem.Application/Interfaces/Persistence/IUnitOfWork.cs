using QROrderSystem.Application.Interfaces.Repositories;

namespace QROrderSystem.Application.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    IOrderRepository Orders { get; }
    IProductRepository Products { get; }
    IOrderItemRepository OrderItems { get; }
    ILocationRepository Locations { get; }
    ICategoryRepository Categories { get; }
    
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}