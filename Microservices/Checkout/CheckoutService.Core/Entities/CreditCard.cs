namespace CheckoutService.Core.Entities;

public class CreditCard : BaseEntity
{
    public CreditCard(string username, ulong cardNumber, bool isDefault = true)
    {
        CardNumber=cardNumber;
        Username = username;
        IsDefault = isDefault;
    }
    public string Username { get; set; }
    
    public ulong CardNumber { get; set; }
    
    public bool IsDefault { get; set; }
}