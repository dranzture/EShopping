using AuthenticationService.Core.Interfaces;
using AuthenticationService.Dtos;
using AuthenticationService.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Core.Services;

public class LoggingUserService : ILoggingUserService
{
    private readonly UserManager<User> _manager;
    private readonly IJwtService _jwtService;

    public LoggingUserService(UserManager<User> manager, IJwtService jwtService)
    {
        _manager = manager;
        _jwtService = jwtService;
    }
    
    public async Task<LoggedUserDto> LoginUser(LoginRequestDto request, CancellationToken token = default)
    {
        var user = await GetUser(request);
        
        if (user == null)
        {
            throw new UnauthorizedAccessException("User/Password combination is incorrect.");
        }

        var passwordResult = await ValidatePassword(user, request);

        if (!passwordResult)
        {
            throw new UnauthorizedAccessException("User/Password combination is incorrect.");
        }

        var userRoles = await GetRoles(user);
        
        var accessToken = await GenerateToken(user, userRoles);

        return new LoggedUserDto()
        {
            AccessToken = accessToken,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }

    public async Task<LoggedUserDto> RefreshToken(User user, IList<string> roles, CancellationToken token = default)
    {
        var accessToken = await _jwtService.GenerateJwtToken(user, roles);
        
        return new LoggedUserDto()
        {
            AccessToken = accessToken,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }
    
    public async Task<User?> GetUser(LoginRequestDto request)
    {
        return await _manager.FindByEmailAsync(request.Username);
    }
    
    public async Task<bool> ValidatePassword(User user, LoginRequestDto request)
    {
        return await _manager.CheckPasswordAsync(user, request.Password);
    }
    
    public async Task<string> GenerateToken(User user, IList<string> Roles)
    {
        return await _jwtService.GenerateJwtToken(user, Roles);
    }
    
    public async Task<IList<string>> GetRoles(User user)
    {
        return await _manager.GetRolesAsync(user);
    }
}