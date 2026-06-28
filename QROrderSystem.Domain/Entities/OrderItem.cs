namespace QROrderSystem.Domain.Entities;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    
    public virtual Order Order { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
}