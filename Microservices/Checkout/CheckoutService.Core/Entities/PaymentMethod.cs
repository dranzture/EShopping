using CheckoutService.Core.ValueObjects;

namespace CheckoutService.Core.Entities;

public class PaymentMethod : BaseEntity
{
    public PaymentMethod() //For ef
    {
    }

    public PaymentMethod(string username, CreditCard? creditCard = null)
    {
        Username = username;
        if (creditCard == null) return;
        if (creditCard.IsDefault)
        {
            UpdateDefaultCreditCard(creditCard);
        }
            
        AddPaymentMethod(creditCard);
    }

    public string Username { get; private set; }

    private readonly List<CreditCard> _creditCards = new();
    
    public IReadOnlyCollection<CreditCard> CreditCards => _creditCards;

    public CreditCard? SelectedCreditCard { get; private set; }

    private void AddPaymentMethod(CreditCard creditCard)
    {
        _creditCards.Add(creditCard);
    }
    
    private void UpdateDefaultCreditCard(CreditCard creditCard)
    {
        SelectedCreditCard = creditCard;
        var defaultCreditCard = _creditCards.FirstOrDefault(e => e.IsDefault);
        if (defaultCreditCard != null)
        {
            defaultCreditCard.IsDefault = false;
        }
    }
}