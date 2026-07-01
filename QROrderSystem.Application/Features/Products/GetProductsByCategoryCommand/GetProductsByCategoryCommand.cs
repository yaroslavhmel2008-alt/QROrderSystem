using MediatR;
using QROrderSystem.Application.DTOs;

namespace QROrderSystem.Application.Features.Products.GetProductsByCategoryCommand;

public class GetProductsByCategoryCommand : IRequest<IEnumerable<ProductDto>>
{
    public Guid CategoryId { get; set; }
}