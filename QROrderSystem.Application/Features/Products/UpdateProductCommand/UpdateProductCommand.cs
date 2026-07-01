using MediatR;
using QROrderSystem.Application.DTOs;

namespace QROrderSystem.Application.Features.Products.UpdateProductCommand;

public class UpdateProductCommand : IRequest<ProductDto>
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsAvailable { get; set; } = true;
}