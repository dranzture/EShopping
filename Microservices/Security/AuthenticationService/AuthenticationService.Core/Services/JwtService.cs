using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthenticationService.Core.Interfaces;
using AuthenticationService.Models;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace AuthenticationService.Core.Services;

public class JwtService : IJwtService
{
    private readonly AppSecrets _appSecrets;

    public JwtService(AppSecrets appSecrets)
    {
        _appSecrets = appSecrets;
    }
    public async Task<string> GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName)
        };
        claims.AddRange(user.UserRoles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

        return await TokenGeneratorAsync(user.UserName, claims.ToArray());
    }

    public Task<string> TokenGeneratorAsync(string username,
        Claim[] additionalClaims = null)
    {
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub,username),
            // this guarantees the token is unique
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        if (additionalClaims is object)
        {
            var claimList = new List<Claim>(claims);
            claimList.AddRange(additionalClaims);
            claims = claimList.ToArray();
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSecrets.JwtSettings.TokenKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(claims),
            
            Issuer = _appSecrets.JwtSettings.Issuer,
            Audience = _appSecrets.JwtSettings.Auidence,
            Expires = DateTime.UtcNow.AddMinutes(_appSecrets.JwtSettings.ExpirationSpan),
            SigningCredentials = creds
        };
        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(jwtSecurityToken);
        
        return Task.Run(()=>tokenHandler.WriteToken(token));
    }
}