using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Domain.Entities;

public class OrderEntity : BaseEntity
{
    public Guid LocationId { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal TotalAmount { get; set; }
    
    public virtual LocationEntity LocationEntity { get; set; } = null!;
    public virtual ICollection<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();
    public virtual ICollection<PaymentEntity> Payments { get; set; } = new List<PaymentEntity>();
}