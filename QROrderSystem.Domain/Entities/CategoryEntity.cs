namespace QROrderSystem.Domain.Entities;

public class CategoryEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public virtual ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
}