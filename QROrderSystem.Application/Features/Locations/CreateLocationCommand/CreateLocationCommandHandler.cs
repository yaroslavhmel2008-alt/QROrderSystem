using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Features.CreateLocationCommand;

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, LocationDto>
{
    private readonly ILocationService _locationService;
    
    public CreateLocationCommandHandler(ILocationService locationService)
    {
        _locationService = locationService;
    }

    public async Task<LocationDto> Handle(CreateLocationCommand command, CancellationToken cancellationToken)
    {
        
        return await _locationService.CreateLocationAsync(command.Name, command.Type, command.IsActive);
    }
}