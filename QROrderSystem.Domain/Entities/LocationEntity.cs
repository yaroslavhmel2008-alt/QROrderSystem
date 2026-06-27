using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Domain.Entities;

public class LocationEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public LocationType Type { get; set; }
    public bool IsActive { get; set; } = true;
    public virtual ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
}