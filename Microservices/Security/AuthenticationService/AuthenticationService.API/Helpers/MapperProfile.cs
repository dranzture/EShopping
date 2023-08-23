using AuthenticationService.Dtos;
using AutoMapper;

namespace AuthenticationService.Helpers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<LoginRequestDto, LoginUserRequest>();
        CreateMap<LoggedUserDto, LoggedUserResponse>();
    }
}