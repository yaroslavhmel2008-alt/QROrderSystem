using QROrderSystem.Application.DTOs;
using QROrderSystem.Domain.Entities;
using QROrderSystem.Domain.Enums;

namespace QROrderSystem.Application.Interfaces.Repositories;

public interface ILocationRepository
{
    Task<LocationEntity?> GetLocationByNameAsync(string name);
    Task<LocationEntity> AddLocationAsync(LocationEntity locationEntity);
}