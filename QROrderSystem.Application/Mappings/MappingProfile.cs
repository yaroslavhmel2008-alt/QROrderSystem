using AutoMapper;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Domain.Entities;

namespace QROrderSystem.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductEntity, ProductDto>().ReverseMap();
        CreateMap<OrderEntity, OrderDto>().ReverseMap();
        CreateMap<CategoryEntity, CategoryDto>().ReverseMap();
        CreateMap<OrderItemEntity, OrderItemDto>().ReverseMap();
        CreateMap<LocationEntity, LocationDto>().ReverseMap();
    }
}