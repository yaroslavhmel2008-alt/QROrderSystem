using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Domain.Entities;

public class Order : BaseEntity
{
    public Guid LocationId { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal TotalAmount { get; set; }
    
    public virtual Location Location { get; set; } = null!;
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}