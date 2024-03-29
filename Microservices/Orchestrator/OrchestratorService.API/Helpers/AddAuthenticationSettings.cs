﻿using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OrchestratorService.Core.ValueObjects;

namespace OrchestratorService.API.Helpers;

public static class AuthenticationSettings
{
    public static void AddAuthenticationSettings(this IServiceCollection builder, AppSecrets secrets)
    {
        builder.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = secrets.JwtSettings.Issuer,
                    ValidAudience = secrets.JwtSettings.Audience,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrets.JwtSettings.TokenKey)),
                    
                };
            });
    }
}