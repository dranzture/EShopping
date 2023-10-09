using AutoMapper;
using GrpcShippingItemService;
using ShippingService.Core.Dto;
using ShippingService.Core.Entities;

namespace ShippingService.API.Helpers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<ShippingItem, ShippingItemDto>().ReverseMap();
        CreateMap<ShippingItemDto, GrpcShippingItemDto>().ReverseMap();
        CreateMap<GrpcUpdateShippingStatusDto, ShippingItemDto>();
    }
}