using CheckoutService.Core.Entities;

namespace CheckoutService.Core.Interfaces;

public interface IPaymentMethodRepository : IRepository<PaymentMethod>
{
    Task<PaymentMethod> GetByUsername(string username, CancellationToken token = default);
    
    Task<HashSet<PaymentMethod>> GetAllByUsername(string username, CancellationToken token = default);
}