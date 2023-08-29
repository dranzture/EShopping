using AuthenticationService.Core.Interfaces;
using AuthenticationService.Dtos;
using AuthenticationService.Models;
using Grpc.Core;
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
        try
        {
            var user = await GetUser(request);
        
            if (user == null)
            {
                Console.WriteLine($"---> Error during login: User is not found.");
                
                throw new RpcException(new Status(StatusCode.Unauthenticated, "User/Password combination is incorrect."));
            }

            var passwordResult = await ValidatePassword(user, request);

            if (!passwordResult)
            {
                Console.WriteLine($"---> Error during login: Bad credentials for user.");
                
                throw new RpcException(new Status(StatusCode.Unauthenticated, "User/Password combination is incorrect."));
            }

            var userRoles = await GetRoles(user);
        
            var accessToken = await GenerateToken(user, userRoles);

            return new LoggedUserDto()
            {
                AccessToken = accessToken,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Roles = userRoles
            };
        }
        catch(Exception ex)
        {
            Console.WriteLine($"---> Error during login: {ex.Message}");
            
            throw new RpcException(new Status(StatusCode.Internal, "Internal Server Error"), ex.Message);
        }

    }

    public async Task<LoggedUserDto> RefreshToken(LoggedUserDto userDto, IList<string> roles, CancellationToken token = default)
    {
        var user = await GetUser(userDto);
        
        if (user == null)
        {
            throw new RpcException(new Status(StatusCode.Unauthenticated, "User is not found"));
        }
        
        var accessToken = await _jwtService.GenerateJwtToken(user, roles);
        
        return new LoggedUserDto()
        {
            AccessToken = accessToken,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.UserName
        };
    }
    
    public async Task<User?> GetUser(LoginRequestDto request)
    {
        var user = await _manager.FindByEmailAsync(request.Username) ?? await _manager.FindByNameAsync(request.Username);
        return user;
    }
    
    public async Task<User?> GetUser(LoggedUserDto request)
    {
        var user = await _manager.FindByEmailAsync(request.Email) ?? await _manager.FindByNameAsync(request.Username);
        return user;
    }
    
    public async Task<bool> ValidatePassword(User user, LoginRequestDto request)
    {
        return await _manager.CheckPasswordAsync(user, request.Password);
    }
    
    public async Task<string> GenerateToken(User user, IList<string> Roles)
    {
        return await _jwtService.GenerateJwtToken(user, Roles);
    }
    
    private async Task<IList<string>> GetRoles(User user)
    {
        return await _manager.GetRolesAsync(user);
    }
}