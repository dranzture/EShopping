using AuthenticationService.Dtos;
using AutoMapper;
using GrpcAuthenticationService;

namespace AuthenticationService.Helpers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<LoginUserRequest, LoginRequestDto>();
        CreateMap<LoggedUserDto, LoggedUserResponse>();
    }
}