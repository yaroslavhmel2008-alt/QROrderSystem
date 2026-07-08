using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Application.DTOs;

public class LocationDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public LocationType Type { get; set; }
    public bool IsActive { get; set; } = true;
}