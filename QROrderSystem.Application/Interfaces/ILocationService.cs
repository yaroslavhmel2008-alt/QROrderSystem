using QROrderSystem.Application.DTOs;
using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Application.Interfaces;

public interface ILocationService
{
    Task<LocationDto> CreateLocationAsync(string Name, LocationType LocationType, bool IsActive);
    Task<IEnumerable<LocationDto>> GetLocationListAsync();
    Task<LocationDto> UpdateLocationAsync(Guid Id, string Name, LocationType LocationType, bool IsActive);
    Task<bool> DeleteLocationAsync(Guid Id);
    
    Task<LocationDto> GetLocationByIdAsync(Guid Id);
}