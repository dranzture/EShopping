using AutoMapper;
using GrpcAuthenticationService;
using GrpcOrderService;
using GrpcShippingItemService;
using OrchestratorService.Core.Dtos;
using OrchestratorService.Core.Dtos.Order;
using OrchestratorService.Core.Dtos.ShippingItem;

namespace OrchestratorService.API.Helpers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<LoginRequestDto, LoginUserRequest>();
        CreateMap<LoggedUserResponse, LoggedUserDto>();

        CreateMap<OrderDto, GrpcOrderDto>().ReverseMap();
        CreateMap<UpdateShippingStatusDto, GrpcUpdateShippingStatusDto>();
        CreateMap<ShippingItemDto, GrpcShippingItemDto>().ReverseMap();
    }
}