using ShippingService.Core.Interfaces;
using ShippingService.Infrastructure.Data;

namespace ShippingService.Infrastructure;

public class EfRepository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public EfRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<IQueryable<T>> Queryable(CancellationToken cancellationToken = default)
    {
        var result = _context.Set<T>().AsQueryable();
        return Task.FromResult(result);
    }

    public async Task AddAsync(T item, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddAsync(item, cancellationToken);
    }

    public Task UpdateAsync(T item, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_context.Set<T>().Update(item));
    }
    

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken) > 0; 
    }
}