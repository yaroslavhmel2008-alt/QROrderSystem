using AutoMapper;
using QROrderSystem.Application.DTOs;
using QROrderSystem.Domain.Entities;

namespace QROrderSystem.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductEntity, ProductDto>().ReverseMap();
        CreateMap<CategoryEntity, CategoryDto>().ReverseMap();
        CreateMap<LocationEntity, LocationDto>().ReverseMap();
        
        CreateMap<OrderEntity, OrderDto>()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.LocationEntity.Name))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ReverseMap();
        
        CreateMap<OrderItemEntity, OrderItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductEntity != null ? src.ProductEntity.Name : "Невідомий товар"))
            .ReverseMap()
            .ForMember(dest => dest.ProductEntity, opt => opt.Ignore());
    }
}