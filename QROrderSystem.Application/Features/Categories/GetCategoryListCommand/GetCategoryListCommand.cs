using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Domain.Entities;
using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Application.Features.GetCategoryListCommand;

public class GetCategoryListCommand : IRequest<IEnumerable<CategoryDto>>
{
}