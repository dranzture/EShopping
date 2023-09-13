using AutoMapper;
using GrpcReviewService;

using ReviewService.Core.Dtos;
using ReviewService.Core.Models;

namespace ReviewService.API.Helpers;

public class MappingProfiles : Profile
{
    //To avoid violations to Clean Architecture. Grpc models -> Core models -> Persistence models
    public MappingProfiles()
    {
        CreateMap<GrpcReviewDto, ReviewDto>()
            .ForMember(dest => dest.Id, opt 
                => opt.MapFrom(src => !string.IsNullOrEmpty(src.Id) ? new Guid(src.Id) : (Guid?)null))
            .ForMember(e => e.InventoryId, t =>
                t.MapFrom(e => new Guid(e.InventoryId))).ReverseMap();
        
        CreateMap<ReviewDto, Review>().ReverseMap();

    }
}