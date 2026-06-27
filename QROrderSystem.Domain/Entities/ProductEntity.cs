namespace QROrderSystem.Domain.Entities;

public class ProductEntity : BaseEntity
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsAvailable { get; set; } = true;
    public virtual CategoryEntity CategoryEntity { get; set; } = null!;
}