using InventoryService.Core.Interfaces;
using InventoryService.Infrastructure.Data;


namespace InventoryService.Infrastructure;

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

    public Task DeleteAsync(T item, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_context.Set<T>().Remove(item));
    }

    public async Task<bool> SaveChangesAsync()
    {
        var semaphore = new SemaphoreSlim(1, 1);
        try
        {
            await semaphore.WaitAsync();
            var result = await _context.SaveChangesAsync() > 0;
            _context.ChangeTracker.Clear();
            return result;
        }
        finally
        {
            semaphore.Release();
        }
    }
}