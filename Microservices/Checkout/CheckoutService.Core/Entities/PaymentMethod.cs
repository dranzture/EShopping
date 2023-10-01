using CheckoutService.Core.ValueObjects;

namespace CheckoutService.Core.Entities;

public class PaymentMethod : BaseEntity
{
    public PaymentMethod() //For ef
    {
    }

    public PaymentMethod(string username)
    {
        Username = username;
        _creditCards = new List<CreditCard>();
    }

    public string Username { get; private set; }

    private readonly List<CreditCard> _creditCards;
    
    public IReadOnlyCollection<CreditCard> CreditCards => _creditCards;
    
    public void AddPaymentMethod(CreditCard creditCard)
    {
        if (creditCard.IsDefault)
        {
            UpdateDefaultCreditCard();
        }
        _creditCards.Add(creditCard);
        SetDefaultCreditCard();
    }
    
    private void UpdateDefaultCreditCard()
    {
        var creditCards = _creditCards.Where(e => e.IsDefault).ToList();
        foreach (var item in creditCards)
        {
            item.IsDefault = false;
        }
    }

    private void SetDefaultCreditCard()
    {
        var creditCards = _creditCards.Count(e => e.IsDefault);
        if (creditCards != 0) return;
        var item = _creditCards.FirstOrDefault();
        if (item == null) return;
        item.IsDefault = true;
    }
}