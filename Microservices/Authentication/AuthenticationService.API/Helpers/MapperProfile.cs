﻿using AuthenticationService.Dtos;
using AutoMapper;

namespace AuthenticationService.Helpers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<LoginUserRequest, LoginRequestDto>();
        CreateMap<LoggedUserDto, LoggedUserResponse>();
    }
}