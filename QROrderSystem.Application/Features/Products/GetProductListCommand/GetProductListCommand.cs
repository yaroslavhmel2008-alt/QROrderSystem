using MediatR;
using QROrderSystem.Application.DTOs;

namespace QROrderSystem.Application.Features.Products.GetProductListCommand;

public class GetProductListCommand : IRequest<IEnumerable<ProductDto>>
{
}