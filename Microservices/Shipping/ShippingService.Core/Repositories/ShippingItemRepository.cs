using Microsoft.EntityFrameworkCore;
using ShippingService.Core.Entities;
using ShippingService.Core.Interfaces;

namespace ShippingService.Core.Repositories;

public class ShippingItemRepository : IShippingItemRepository
{
    private readonly IRepository<ShippingItem> _context;

    public ShippingItemRepository(IRepository<ShippingItem> context)
    {
        _context = context;
    }

    public async Task<IQueryable<ShippingItem>> Queryable(CancellationToken cancellationToken = default)
    {
        return await _context.Queryable(cancellationToken);
    }

    public async Task AddAsync(ShippingItem item, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(item, cancellationToken);
    }

    public async Task UpdateAsync(ShippingItem item, CancellationToken cancellationToken = default)
    {
        await _context.UpdateAsync(item, cancellationToken);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<HashSet<ShippingItem>> GetAllShippingItems(CancellationToken token = default)
    {
        var result = await _context.Queryable(token);
        return result.AsNoTracking().ToHashSet();
    }

    public async Task<ShippingItem?> GetById(Guid id, CancellationToken token = default)
    {
        var result = await _context.Queryable(token);
        return await result.AsNoTracking().FirstOrDefaultAsync(e => e.Id==id, token);
    }

    public async Task<ShippingItem?> GetByOrderId(Guid id, CancellationToken token = default)
    {
        var result = await _context.Queryable(token);
        return await result.AsNoTracking().FirstOrDefaultAsync(e => e.OrderId==id, token);
    }
}