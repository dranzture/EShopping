using AutoMapper;
using GrpcInventoryService;
using GrpcReviewService;
using InvShopRevOrchestrator.Core.Dtos.Inventory;
using InvShopRevOrchestrator.Core.Dtos.Review;

namespace InvShopRevOrchestrator.API.Helpers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UpdateInventoryDto, GrpcUpdateInventoryDto>();
        
        CreateMap<GrpcInventoryDto, InventoryDto>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Id) ? (Guid?)null : Guid.Parse(src.Id)))
            .ReverseMap(); // Reverse mapping from InventoryDto to GrpcInventoryDto
        
        CreateMap<InventoryQuantityChangeDto, GrpcInventoryQuantityChangeDto>()
            .ReverseMap();
        
        CreateMap<GrpcReviewDto, ReviewDto>()
            .ForMember(dest => dest.Id, opt
                => opt.MapFrom(src => !string.IsNullOrEmpty(src.Id) ? new Guid(src.Id) : (Guid?)null))
            .ForMember(e => e.InventoryId, t =>
                t.MapFrom(e => new Guid(e.InventoryId)))
            .ReverseMap();
    }
}