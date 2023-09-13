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

    public async Task UpdateAsync(T item, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => _context.Set<T>().Update(item), cancellationToken);
    }

    public async Task DeleteAsync(T item, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => _context.Set<T>().Remove(item), cancellationToken);
    }

    public async Task<bool> SaveChangesAsync()
    {
       return await _context.SaveChangesAsync() > 0;
    }
}