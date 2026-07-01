using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Features.UpdateLocationCommand;

public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, LocationDto>
{
    private readonly ILocationService _locationService;
    
    public UpdateLocationCommandHandler(ILocationService locationService)
    {
        _locationService = locationService;
    }

    public async Task<LocationDto> Handle(UpdateLocationCommand command, CancellationToken cancellationToken)
    {
        var location = await _locationService.UpdateLocationAsync(command.Id, command.Name, command.Type, command.IsActive);
        if (location == null)
        {
            throw new Exception($"Location {command.Id} does not exist");
        }
        
        return location;
    }
}