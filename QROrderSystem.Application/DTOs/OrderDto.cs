namespace QROrderSystem.Application.DTOs;

public class OrderDto : BaseDto
{
    public Guid LocationId { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItemDto> OrderItems { get; set; } = new();
    public string LocationName { get; set; }
    public string Status { get; set; }
}