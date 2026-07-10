using QROrderSystem.Application.DTOs;
using QROrderSystem.Domain.Entities;
using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Application.Interfaces.Repositories;

public interface ILocationRepository
{
    Task<LocationEntity?> GetLocationByNameAsync(string name);
    Task<LocationEntity> AddLocationAsync(LocationEntity locationEntity);
    Task<IEnumerable<LocationEntity>> GetLocationListAsync();
    Task<LocationEntity?> GetLocationByIdAsync(Guid Id);
    Task<LocationEntity?> UpdateLocationAsync(LocationEntity locationEntity);
    Task<bool> DeleteLocationAsync(LocationEntity locationEntity);
}