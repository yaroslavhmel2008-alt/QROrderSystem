using MediatR;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Domain.Entities;
using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Application.Features.UpdateCategoryCommand;

public class UpdateCategoryCommand : IRequest<CategoryDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}