using MediatR;

namespace QROrderSystem.Application.Features.DeleteOrderCommand;

public class DeleteOrderCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}