using MediatR;
using QROrderSystem.Application.DTOs;

namespace QROrderSystem.Application.Features.Products.DeleteProductCommand;

public class DeleteProductCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}