namespace QROrderSystem.Application.DTOs;

public class CategoryDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}