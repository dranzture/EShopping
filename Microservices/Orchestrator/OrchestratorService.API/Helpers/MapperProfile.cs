using AuthenticationService;
using AutoMapper;
using OrchestratorService.Core.Dtos;

namespace OrchestratorService.API.Helpers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<LoginRequestDto, LoginUserRequest>();
        CreateMap<LoggedUserResponse, LoggedUserDto>();
    }
}