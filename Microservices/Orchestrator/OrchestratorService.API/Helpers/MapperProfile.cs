using AutoMapper;
using GrpcAuthenticationService;
using GrpcInventoryService;
using GrpcReviewService;
using OrchestratorService.Core.Dtos;
using OrchestratorService.Core.Dtos.Inventory;
using OrchestratorService.Core.Dtos.Review;

namespace OrchestratorService.API.Helpers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<LoginRequestDto, LoginUserRequest>();
        CreateMap<LoggedUserResponse, LoggedUserDto>();

        CreateMap<MutateInventoryDto, GrpcMutateInventoryDto>();

        CreateMap<GrpcInventoryDto, InventoryDto>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Id) ? (Guid?)null : Guid.Parse(src.Id)))
            .ReverseMap(); // Reverse mapping from InventoryDto to GrpcInventoryDto

        CreateMap<ChangeInventoryDto, GrpcInventoryChangeDto>()
            .ReverseMap();

        CreateMap<GrpcReviewDto, ReviewDto>()
            .ForMember(dest => dest.Id, opt
                => opt.MapFrom(src => !string.IsNullOrEmpty(src.Id) ? new Guid(src.Id) : (Guid?)null))
            .ForMember(e => e.InventoryId, t =>
                t.MapFrom(e => new Guid(e.InventoryId)))
            .ReverseMap();
    }
}