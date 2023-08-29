using AuthenticationService.Dtos;
using AutoMapper;

namespace AuthenticationService.Helpers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<LoginUserRequest, LoginRequestDto>()
            .ForMember(e=>e.Username, t=>
                t.MapFrom(e=>e.Email));
        CreateMap<LoggedUserDto, LoggedUserResponse>();
    }
}