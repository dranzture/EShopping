using AuthenticationService;
using AutoMapper;
using GrpcInventoryService;
using OrchestratorService.Core.Dtos;
using OrchestratorService.Core.Dtos.Inventory;

namespace OrchestratorService.API.Helpers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<LoginRequestDto, LoginUserRequest>();
        CreateMap<LoggedUserResponse, LoggedUserDto>();

        CreateMap<MutateInventoryDto, GrpcMutateInventoryDto>();

        CreateMap<GrpcInventoryDto, InventoryDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Id) ? (Guid?)null : Guid.Parse(src.Id)))
            .ReverseMap(); // Reverse mapping from InventoryDto to GrpcInventoryDto

        CreateMap<ChangeInventoryDto, GrpcInventoryChangeDto>()
            .ReverseMap();
    }
}