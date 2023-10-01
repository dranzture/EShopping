namespace CheckoutService.Core.Entities;

public class CreditCard : BaseEntity
{
    public CreditCard(string username, ulong cardNumber, bool isValidated = true)
    {
        CardNumber=cardNumber;
        Username = username;
        IsValidated = isValidated;
    }
    public string Username { get; set; }
    
    public ulong CardNumber { get; set; }
    
    public bool IsValidated { get; set; }
}