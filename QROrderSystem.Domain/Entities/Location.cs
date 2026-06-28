using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Domain.Entities;

public class Location : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public LocationType Type { get; set; }
    public bool IsActive { get; set; } = true;
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}