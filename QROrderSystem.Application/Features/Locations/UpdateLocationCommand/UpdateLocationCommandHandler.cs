using MediatR;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Exceptions;
using QROrderSystem.Application.Interfaces.Services;

namespace QROrderSystem.Application.Features.UpdateLocationCommand;

public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, LocationDto>
{
    private readonly ILocationService _locationService;
    private readonly ILogger<UpdateLocationCommandHandler> _logger;
    
    public UpdateLocationCommandHandler(ILocationService locationService,  ILogger<UpdateLocationCommandHandler> logger)
    {
        _locationService = locationService;
        _logger = logger;
    }

    public async Task<LocationDto> Handle(UpdateLocationCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating location with ID: {LocationId}", command.Id);
        var location = await _locationService.UpdateLocationAsync(command.Id, command.Name, command.Type, command.IsActive);
        if (location == null)
        {
            _logger.LogWarning("Location with ID {LocationId} was not found", command.Id);
            throw new NotFoundException("Location", command.Id);
        }
        
        _logger.LogInformation("Successfully updated location with ID: {LocationId}", command.Id);
        return location;
    }
}