using MediatR;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces.Services;

namespace QROrderSystem.Application.Features.CreateLocationCommand;

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, LocationDto>
{
    private readonly ILocationService _locationService;
    private readonly ILogger<CreateLocationCommandHandler> _logger;
    
    public CreateLocationCommandHandler(ILocationService locationService, ILogger<CreateLocationCommandHandler> logger)
    {
        _locationService = locationService;
        _logger = logger;
    }

    public async Task<LocationDto> Handle(CreateLocationCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating location: {Name}, Type: {Type}", command.Name, command.Type);
    
        var location = await _locationService.CreateLocationAsync(command.Name, command.Type, command.IsActive);
    
        _logger.LogInformation("Location '{Name}' created successfully with ID: {Id}", location.Name, location.Id);
    
        return location;
    }
}