using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Domain.Entities;
using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Application.Features.DeleteLocationCommand;

public class DeleteLocationCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}