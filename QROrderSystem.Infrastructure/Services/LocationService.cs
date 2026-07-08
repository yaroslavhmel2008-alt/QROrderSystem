using AutoMapper;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Exceptions;
using QROrderSystem.Application.Interfaces.Persistence;
using QROrderSystem.Application.Interfaces.Repositories;
using QROrderSystem.Application.Interfaces.Services;
using QROrderSystem.Domain.Entities;
using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Infrastructure.Services;

public class LocationService : ILocationService
{
    private readonly ILogger<LocationService> _logger;
    private readonly IMapper _mapper;
    private readonly ILocationRepository _locationRepository;
    private readonly IUnitOfWork _unitOfWork;
    public LocationService(ILogger<LocationService> logger, IMapper mapper, ILocationRepository locationRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _locationRepository = locationRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<LocationDto> CreateLocationAsync(string Name, LocationType LocationType, bool IsActive)
    {
        var existingLocation = await _locationRepository.GetLocationByNameAsync(Name);
        if (existingLocation != null)
        {
            _logger.LogWarning("Location with name {Name} already exists", Name);
            throw new BadRequestException("Location", $"Location with name '{Name}' already exists");
        }

        var newLocation = new LocationEntity()
        {
            Id = Guid.NewGuid(),
            Name = Name,
            Type = LocationType,
            IsActive = IsActive
        };
        
        await _locationRepository.AddLocationAsync(newLocation);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Location {Name} created successfully with ID {Id}", Name, newLocation.Id);
        return _mapper.Map<LocationDto>(newLocation);
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