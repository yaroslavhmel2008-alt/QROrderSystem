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

    public async Task<IEnumerable<LocationDto>> GetLocationListAsync()
    {
        _logger.LogInformation("GetLocationListAsync");
        var locations = await _locationRepository.GetLocationListAsync();
        var locationList = locations.ToList();
        _logger.LogInformation("Successfully retrieved {Count} locations", locationList.Count);
        return _mapper.Map<IEnumerable<LocationDto>>(locationList);
    }

    public async Task<LocationDto> UpdateLocationAsync(Guid Id, string Name, LocationType LocationType, bool IsActive)
    {
        _logger.LogInformation("UpdateLocationAsync");
        var existingLocation = await _locationRepository.GetLocationByIdAsync(Id);
        if (existingLocation == null)
        {
            _logger.LogWarning("Location with ID {Id} not found", Id);
            throw new NotFoundException("Location", $"Location with ID '{Id}' not found");
        }
        
        existingLocation.Name = Name;
        existingLocation.Type = LocationType;
        existingLocation.IsActive = IsActive;
        
        var location = await _locationRepository.UpdateLocationAsync(existingLocation);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Location {Name} (ID: {Id}) updated successfully", Name, Id);
        return _mapper.Map<LocationDto>(location);
    }

    public async Task<bool> DeleteLocationAsync(Guid Id)
    {
        var location = await _locationRepository.GetLocationByIdAsync(Id);
        if (location == null)
        {
            _logger.LogWarning("Location with ID {Id} not found", Id);
            throw new NotFoundException("Location", $"Location with ID '{Id}' not found");
        }
        var isDeleted = await _locationRepository.DeleteLocationAsync(location);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Location with ID {Id} deleted successfully", Id);
        return isDeleted;
    }

    public async Task<LocationDto> GetLocationByIdAsync(Guid Id)
    {
        var location = await _locationRepository.GetLocationByIdAsync(Id);
        if (location == null)
        {
            _logger.LogWarning("Location with ID {Id} not found", Id);
            throw new NotFoundException("Location", $"Location with ID '{Id}' not found");
        }
        
        return _mapper.Map<LocationDto>(location);
    }
}