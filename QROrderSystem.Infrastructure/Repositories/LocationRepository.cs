using Microsoft.EntityFrameworkCore;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces.Repositories;
using QROrderSystem.Domain.Entities;
using QROrderSystem.Infrastructure.Persistence;

namespace QROrderSystem.Infrastructure.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly ApplicationDbContext _context;
    public LocationRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<LocationEntity?> GetLocationByNameAsync(string name)
    {
        return await _context.Locations.FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task<LocationEntity> AddLocationAsync(LocationEntity locationEntity)
    {
        await _context.Locations.AddAsync(locationEntity);
        return locationEntity;
    }

    public async Task<IEnumerable<LocationEntity>> GetLocationListAsync()
    {
        return await _context.Locations.ToListAsync();
    }

    public async Task<LocationEntity?> GetLocationByIdAsync(Guid Id)
    {
        return await _context.Locations.FindAsync(Id);
    }

    public Task<LocationEntity?> UpdateLocationAsync(LocationEntity locationEntity)
    {
        _context.Locations.Update(locationEntity);
        return Task.FromResult(locationEntity);
    }

    public Task<bool> DeleteLocationAsync(LocationEntity locationEntity)
    {
        _context.Locations.Remove(locationEntity);
        return Task.FromResult(true);
    }
}