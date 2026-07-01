namespace QROrderSystem.Domain.Entities;

public class OrderItemEntity : BaseEntity
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    
    public virtual OrderEntity OrderEntity { get; set; } = null!;
    public virtual ProductEntity ProductEntity { get; set; } = null!;
}