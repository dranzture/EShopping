using AutoMapper;
using ShippingService.Core.Dto;
using ShippingService.Core.Entities;

namespace ShippingService.API.Helpers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<ShippingItem, ShippingItemDto>().ReverseMap();
    }
}