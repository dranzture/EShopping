﻿using AuthenticationService.Dtos;
using AuthenticationService.Models;

namespace AuthenticationService.Core.Interfaces;

public interface ILoggingUserService
{
    Task<LoggedUserDto> LoginUser(LoginRequestDto request, CancellationToken token = default);

    Task<LoggedUserDto> RefreshToken(LoggedUserDto user, IList<string> roles, CancellationToken token = default);
    
    Task<User?> GetUser(LoginRequestDto request);
    
    Task<User?> GetUser(LoggedUserDto request);
    
    Task<bool> ValidatePassword(User user, LoginRequestDto request);
    
    Task<string> GenerateToken(User user, IList<string> roles);
}
