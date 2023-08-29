using AuthenticationService;
using AutoMapper;
using OrchestratorService.Core.Dtos;

namespace OrchestratorService.Helpers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<LoginRequestDto, LoginUserRequest>().ForMember(e => e.Email,
            t =>
                t.MapFrom(e => e.Username));
        CreateMap<LoggedUserResponse, LoggedUserDto>();
    }
}