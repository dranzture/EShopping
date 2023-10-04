using AutoMapper;
using OrderService.Core.Dtos;
using OrderService.Core.Entities;

namespace OrderService.API.Helpers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<OrderDto, Order>().ReverseMap();
    }
}