namespace QROrderSystem.Application.DTOs;

public class OrderDto : BaseDto
{
    public Guid Id { get; set; }
    public Guid LocationId { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItemDto> OrderItems { get; set; } = new();
}