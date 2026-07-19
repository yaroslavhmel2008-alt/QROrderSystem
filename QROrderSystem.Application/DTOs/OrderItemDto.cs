using QROrderSystem.Domain.Entities;

namespace QROrderSystem.Application.DTOs;

public class OrderItemDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
}