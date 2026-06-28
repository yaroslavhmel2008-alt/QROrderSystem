using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Domain.Entities;
using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Application.Features.GetCategoryByIdCommand;

public class GetCategoryByIdCommand : IRequest<CategoryDto>
{
    public Guid Id { get; set; }
}