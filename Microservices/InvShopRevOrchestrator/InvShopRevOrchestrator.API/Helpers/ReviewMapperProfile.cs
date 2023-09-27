using AutoMapper;
using GrpcReviewService;
using InvShopRevOrchestrator.Core.Dtos;

namespace InvShopRevOrchestrator.API.Helpers;

public class ReviewMapperProfile : Profile
{
    public ReviewMapperProfile()
    {
        CreateMap<GrpcReviewDto, ReviewDto>()
            .ForMember(dest => dest.Id, opt
                => opt.MapFrom(src => !string.IsNullOrEmpty(src.Id) ? new Guid(src.Id) : (Guid?)null))
            .ForMember(e => e.InventoryId, t =>
                t.MapFrom(e => new Guid(e.InventoryId)))
            .ReverseMap();
    }
}