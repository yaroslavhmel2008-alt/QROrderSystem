using MediatR;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces.Services;

namespace QROrderSystem.Application.Features.GetLocationListCommand;

public class GetLocationListCommandHandler : IRequestHandler<GetLocationListCommand, IEnumerable<LocationDto>>
{
    private readonly ILocationService _locationService;
    private readonly ILogger<GetLocationListCommandHandler> _logger;
    
    public GetLocationListCommandHandler(ILocationService locationService,  ILogger<GetLocationListCommandHandler> logger)
    {
        _locationService = locationService;
        _logger = logger;
    }

    public async Task<IEnumerable<LocationDto>> Handle(GetLocationListCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching the list of locations");
        var locations = await _locationService.GetLocationListAsync();
        var locationList = locations.ToList();
        _logger.LogInformation("Successfully retrieved {Count} locations", locationList.Count);
        return locationList;
    }
}