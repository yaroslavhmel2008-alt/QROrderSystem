using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Features.GetLocationListCommand;

public class GetLocationListCommandHandler : IRequestHandler<GetLocationListCommand, IEnumerable<LocationDto>>
{
    private readonly ILocationService _locationService;
    
    public GetLocationListCommandHandler(ILocationService locationService)
    {
        _locationService = locationService;
    }

    public async Task<IEnumerable<LocationDto>> Handle(GetLocationListCommand command, CancellationToken cancellationToken)
    {
        
        return await _locationService.GetLocationListAsync();
    }
}