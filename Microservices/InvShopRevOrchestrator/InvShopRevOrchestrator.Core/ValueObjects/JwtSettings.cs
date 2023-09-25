namespace InvShopRevOrchestrator.Core.ValueObjects;

public class JwtSettings
{
    public string Audience { get; set; }
    
    public string Issuer { get; set; }
    
    public string TokenKey { get; set; }
}