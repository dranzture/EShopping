﻿namespace AuthenticationService.Models;

public class JwtSettings
{
    public string Audience { get; set; }
    
    public int ExpirationSpan { get; set; } //In minutes
    
    public string Issuer { get; set; }
    
    public string TokenKey { get; set; }
}