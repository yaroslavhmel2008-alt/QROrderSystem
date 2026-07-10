using System.ComponentModel.DataAnnotations.Schema;
using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Domain.Entities;

public class OrderEntity : BaseEntity
{
    public Guid LocationId { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal TotalAmount { get; set; }
    [ForeignKey(nameof(LocationId))]
    public LocationEntity LocationEntity { get; set; } = null!;
    public ICollection<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();
    public ICollection<PaymentEntity> Payments { get; set; } = new List<PaymentEntity>();
}