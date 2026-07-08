using MediatR;
using Microsoft.Extensions.Logging;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Application.Exceptions;
using QROrderSystem.Application.Interfaces.Services;

namespace QROrderSystem.Application.Features.DeleteLocationCommand;

public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, bool>
{
    private readonly ILocationService _locationService;
    private readonly ILogger<DeleteLocationCommandHandler> _logger;
    
    public DeleteLocationCommandHandler(ILocationService locationService, ILogger<DeleteLocationCommandHandler> logger)
    {
        _locationService = locationService;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteLocationCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to delete location with ID: {LocationId}", command.Id);
        var isDeleted = await _locationService.DeleteLocationAsync(command.Id);
        if (!isDeleted)
        {
            _logger.LogError("Failed to delete location with ID: {LocationId}", command.Id);
            throw new NotFoundException("Location", command.Id);
        }
        _logger.LogInformation("Successfully deleted location with ID: {LocationId}", command.Id);
        return isDeleted;
    }
}