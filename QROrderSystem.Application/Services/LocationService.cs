using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;
using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Application.Services;

public class LocationService : ILocationService
{
    public Task<LocationDto> CreateLocationAsync(string Name, LocationType LocationType, bool IsActive)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<LocationDto>> GetLocationListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<LocationDto> UpdateLocationAsync(Guid Id, string Name, LocationType LocationType, bool IsActive)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteLocationAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<LocationDto> GetLocationByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }
}