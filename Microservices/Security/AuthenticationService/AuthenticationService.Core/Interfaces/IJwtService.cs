using AuthenticationService.Models;

namespace AuthenticationService.Core.Interfaces;

public interface IJwtService
{
    Task<string> GenerateJwtToken(User user);
}