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
    
    public async Task<string> GenerateJwtToken(User user, IList<string> Roles)
    {
        var claims = new List<Claim>
        {
            new Claim("id", user.Id.ToString()),
            new Claim("email", user.Email),
            new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
            new Claim("firstName", user.FirstName),
            new Claim("lastName", user.LastName)
        };
        claims.AddRange(Roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return await TokenGeneratorAsync(claims.ToArray());
    }

    private Task<string> TokenGeneratorAsync(
        IEnumerable<Claim>? additionalClaims = null)
    {
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        if (additionalClaims != null)
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
            Audience = _appSecrets.JwtSettings.Audience,
            Expires = DateTime.UtcNow.AddMinutes(_appSecrets.JwtSettings.ExpirationSpan),
            SigningCredentials = creds
        };
        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(jwtSecurityToken);
        return Task.FromResult(tokenHandler.WriteToken(token));
    }
}