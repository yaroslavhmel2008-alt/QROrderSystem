using MediatR;

namespace QROrderSystem.Application.Features.DeleteCategoryCommand;

public class DeleteCategoryCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}