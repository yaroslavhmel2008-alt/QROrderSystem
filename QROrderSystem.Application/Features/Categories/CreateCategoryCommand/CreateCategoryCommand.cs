using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Domain.Entities;
using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Application.Features.CreateCategoryCommand;

public class CreateCategoryCommand : IRequest<CategoryDto>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}