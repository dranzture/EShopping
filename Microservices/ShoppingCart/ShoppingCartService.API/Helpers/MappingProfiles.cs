using System;
using AutoMapper;
using GrpcShoppingCartService;
using ShoppingCartService.Core.Dtos;
using ShoppingCartService.Core.Entities;

namespace ShoppingCartService.API.Helpers;

public class MappingProfiles : Profile
{
    //To avoid violations to Clean Architecture. Grpc models -> Core models -> Persistence models
    public MappingProfiles()
    {
        CreateMap<GrpcInventoryDto, InventoryDto>()
            .ForMember(e => e.Id, t =>
                t.MapFrom(e => new Guid(e.Id)));

        CreateMap<ShoppingItem, ShoppingItemDto>();

        CreateMap<ShoppingItemDto, GrpcShoppingItemDto>()
            .ForMember(e => e.ShoppingCartId, t =>
                t.MapFrom(e => e.ShoppingCartId.ToString()))
            .ForMember(e => e.InventoryId, t =>
                t.MapFrom(e => e.InventoryId.ToString()));
        
        CreateMap<InventoryDto, Inventory>();

        CreateMap<Inventory, GrpcInventoryDto>().ForMember(e => e.Id, t =>
            t.MapFrom(e => e.Id.ToString()));

        CreateMap<GrpcInventoryDto, InventoryDto>()
            .ForMember(dest => dest.Id, opt
                => opt.MapFrom(src => !string.IsNullOrEmpty(src.Id) ? new Guid(src.Id) : (Guid?)null));

        CreateMap<ShoppingCartDto, ShoppingCart>()
            .ReverseMap();

        CreateMap<InventoryDto, GrpcInventoryDto>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id.HasValue ? src.Id.Value.ToString() : string.Empty));

        CreateMap<GrpcShoppingCartDto, ShoppingCartDto>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Id) ? new Guid(src.Id) : (Guid?)null));

        CreateMap<ShoppingCartDto, GrpcShoppingCartDto>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id.HasValue ? src.Id.Value.ToString() : string.Empty));
        
    }
}