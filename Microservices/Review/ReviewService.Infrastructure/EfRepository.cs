using ReviewService.Core.Interfaces;
using ReviewService.Infrastructure.Data;


namespace ReviewService.Infrastructure;

public class EfRepository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public EfRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> Queryable(CancellationToken cancellationToken = default)
    {
        return _context.Set<T>();
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

    public async Task<bool> SaveChanges(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}