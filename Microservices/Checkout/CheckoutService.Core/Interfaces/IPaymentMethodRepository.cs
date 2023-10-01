using CheckoutService.Core.Entities;

namespace CheckoutService.Core.Interfaces;

public interface IPaymentMethodRepository : IRepository<PaymentMethod>
{
    Task<CreditCard?> GetDefaultCreditCardByUsername(string username, CancellationToken token = default);
    
    Task<HashSet<CreditCard>> GetAllCreditCardsByUsername(string username, CancellationToken token = default);
}