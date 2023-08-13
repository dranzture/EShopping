using AuthenticationService.Dtos;
using AuthenticationService.Models;

namespace AuthenticationService.Core.Interfaces;

public interface ILoggingUserService
{
    Task<LoggedUserDto> LoginUser(LoginRequestDto request, CancellationToken token = default);

    Task<LoggedUserDto> RefreshToken(User user, CancellationToken token = default);
    
    Task<User?> GetUser(LoginRequestDto request);
    
    Task<bool> ValidatePassword(User user, LoginRequestDto request);
    
    Task<string> GenerateToken(User user);
}
