using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Interfaces;

namespace QROrderSystem.Application.Features.DeleteLocationCommand;

public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, bool>
{
    private readonly ILocationService _locationService;
    
    public DeleteLocationCommandHandler(ILocationService locationService)
    {
        _locationService = locationService;
    }

    public async Task<bool> Handle(DeleteLocationCommand command, CancellationToken cancellationToken)
    {
        
        var isDeleted = await _locationService.DeleteLocationAsync(command.Id);
        if (!isDeleted)
        {
            throw new Exception($"Location {command.Id} does not exist");
        }
        return isDeleted;
    }
}