using AutoMapper;
using GrpcShoppingCartService;
using InvShopRevOrchestrator.Core.Dtos;

namespace InvShopRevOrchestrator.API.Mappers;

public class ShoppingCartMapperProfile : Profile
{
    public ShoppingCartMapperProfile()
    {
        CreateMap<InventoryDto, GrpcInventoryDto>()
            .ForMember(dest => dest.Id,
                opt =>
                    opt.MapFrom(src => src.Id.HasValue ? src.Id.ToString() : null));
        
        CreateMap<GrpcShoppingCartDto, ShoppingCartDto>().ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Id) ? (Guid?)null : Guid.Parse(src.Id)))
            .ReverseMap();

        CreateMap<GrpcShoppingItemDto, ShoppingItemDto>()            
            .ForMember(e => e.ShoppingCartId, t =>
                t.MapFrom(e => new Guid(e.ShoppingCartId)))
            .ForMember(e => e.InventoryId, t =>
                t.MapFrom(e => new Guid(e.InventoryId)));
        
        CreateMap<AddShoppingCartItemCommandDto, GrpcAddShoppingCartItemCommandDto>();
        CreateMap<UpdateShoppingCartItemCommandDto, GrpcUpdateShoppingCartItemCommandDto>();
        CreateMap<DeleteShoppingCartItemCommandDto, GrpcDeleteShoppingCartItemCommandDto>();
    }
}