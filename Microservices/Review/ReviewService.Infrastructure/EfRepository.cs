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

    public async Task Create(T item, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddAsync(item,cancellationToken);
    }

    public Task Update(T item, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().Update(item);
        return Task.CompletedTask;
    }

    public Task Delete(T item, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().Remove(item);
        return Task.CompletedTask;
    }

    public async Task<bool> SaveChanges(CancellationToken cancellationToken = default)
    {
       return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}