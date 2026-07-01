using MediatR;
using QROrderSystem.Application.DTOs;


namespace QROrderSystem.Application.Features.GetLocationListCommand;

public class GetLocationListCommand : IRequest<IEnumerable<LocationDto>>
{
}