using AutoMapper;
using GrpcInventoryService;
using InvShopRevOrchestrator.Core.Dtos;

namespace InvShopRevOrchestrator.API.Mappers;

public class InventoryMapperProfile : Profile
{
    public InventoryMapperProfile()
    {
        CreateMap<GrpcInventoryDto, InventoryDto>()
            .ForMember(dest => dest.Id,
                opt =>
                    opt.MapFrom(src => string.IsNullOrEmpty(src.Id) ? (Guid?)null : Guid.Parse(src.Id)));

        CreateMap<InventoryDto, GrpcInventoryDto>()
            .ForMember(dest => dest.Id,
                opt =>
                    opt.MapFrom(src => src.Id.HasValue ? src.Id.ToString() : null));
            


        CreateMap<InventoryQuantityChangeRequestDto, GrpcInventoryQuantityChangeDto>()
            .ReverseMap();

        CreateMap<InventoryWithUsernameDto, GrpcInventoryWithUsernameDto>();


        CreateMap<GrpcListInventoryDto, InventoryListItemDto>().ForMember(dest => dest.Id,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Id) ? (Guid?)null : Guid.Parse(src.Id)));

    }
}