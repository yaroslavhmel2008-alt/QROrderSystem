using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Domain.Entities;

public class Payment : BaseEntity
{
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public string? ExternalTransactionId { get; set; }
    public string Provider { get; set; } = string.Empty;
    
    public virtual Order Order { get; set; } = null!;
}