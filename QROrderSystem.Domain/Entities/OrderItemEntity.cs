using System.ComponentModel.DataAnnotations.Schema;

namespace QROrderSystem.Domain.Entities;

public class OrderItemEntity : BaseEntity
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    [ForeignKey(nameof(ProductId))]
    public ProductEntity ProductEntity { get; set; } = null!;
}