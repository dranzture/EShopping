﻿using AutoMapper;
using GrpcInventoryService;
using GrpcReviewService;
using GrpcShoppingCartService;
using InvShopRevOrchestrator.Core.Dtos;
using GrpcInventoryDto = GrpcInventoryService.GrpcInventoryDto;

namespace InvShopRevOrchestrator.API.Helpers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<GrpcInventoryDto, InventoryDto>()
            .ForMember(dest => dest.Id,
                opt =>
                    opt.MapFrom(src => string.IsNullOrEmpty(src.Id) ? (Guid?)null : Guid.Parse(src.Id)));
        
        CreateMap<InventoryDto, GrpcInventoryDto>()
            .ForMember(dest => dest.Id,
                opt =>
                    opt.MapFrom(src => src.Id.ToString()));
        
        CreateMap<InventoryQuantityChangeRequestDto, GrpcInventoryQuantityChangeDto>()
            .ReverseMap();

        CreateMap<InventoryWithUsernameDto, GrpcInventoryWithUsernameDto>();
          

        CreateMap<GrpcListInventoryDto, InventoryListItemDto>().ForMember(dest => dest.Id,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Id) ? (Guid?)null : Guid.Parse(src.Id)));
        
        CreateMap<GrpcReviewDto, ReviewDto>()
            .ForMember(dest => dest.Id, opt
                => opt.MapFrom(src => !string.IsNullOrEmpty(src.Id) ? new Guid(src.Id) : (Guid?)null))
            .ForMember(e => e.InventoryId, t =>
                t.MapFrom(e => new Guid(e.InventoryId)))
            .ReverseMap();
        
        CreateMap<GrpcShoppingCartDto, ShoppingCartDto>().ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Id) ? (Guid?)null : Guid.Parse(src.Id)))
            .ReverseMap(); 
        
        
        CreateMap<GrpcAddShoppingCartItemCommandDto, AddShoppingCartItemCommandDto>();
        CreateMap<GrpcUpdateShoppingCartItemCommandDto, UpdateShoppingCartItemCommandDto>();
        CreateMap<GrpcDeleteShoppingCartItemCommandDto, DeleteShoppingCartItemCommandDto>();
    }
}