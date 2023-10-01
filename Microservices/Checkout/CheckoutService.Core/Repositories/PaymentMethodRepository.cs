using CheckoutService.Core.Entities;
using CheckoutService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CheckoutService.Core.Repositories;

public class PaymentMethodRepository : IPaymentMethodRepository
{
    private readonly IRepository<PaymentMethod> _context;

    public PaymentMethodRepository(IRepository<PaymentMethod> context)
    {
        _context = context;
    }

    public async Task<IQueryable<PaymentMethod>> Queryable(CancellationToken cancellationToken = default)
    {
        return await _context.Queryable(cancellationToken);
    }

    public async Task AddAsync(PaymentMethod item, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(item, cancellationToken);
    }

    public async Task UpdateAsync(PaymentMethod item, CancellationToken cancellationToken = default)
    {
        await _context.UpdateAsync(item, cancellationToken);
    }

    public async Task DeleteAsync(PaymentMethod item, CancellationToken cancellationToken = default)
    {
        await _context.DeleteAsync(item, cancellationToken);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<CreditCard?> GetDefaultCreditCardByUsername(string username, CancellationToken token = default)
    {
        var result = await Queryable(token);
        var paymentMethod = await result
            .AsNoTracking()
            .Include(e => e.CreditCards)
            .Where(e => e.Username == username)
            .FirstOrDefaultAsync(token);
        if (paymentMethod == null || paymentMethod.CreditCards.Count <= 0)
        {
            return null;
        }

        return paymentMethod.CreditCards.FirstOrDefault(e => e.IsDefault);
    }

    public async Task<HashSet<CreditCard>> GetAllCreditCardsByUsername(string username,
        CancellationToken token = default)
    {
        var result = await Queryable(token);
        var paymentMethod = await result
            .AsNoTracking()
            .Include(e=>e.CreditCards)
            .Where(e => e.Username == username)
            .FirstOrDefaultAsync(token);
        return paymentMethod?.CreditCards.ToHashSet() ?? new HashSet<CreditCard>();
    }
}