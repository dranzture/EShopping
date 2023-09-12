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

    public IQueryable<T> Queryable(CancellationToken cancellationToken = default)
    {
        return _context.Set<T>();
    }

    public T Create(T item, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().Add(item);
        SaveChanges();
        return item;
    }

    public void Update(T item, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().Update(item);
    }

    public void Delete(T item, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().Remove(item);
    }

    public bool SaveChanges()
    {
       return _context.SaveChanges() > 0;
    }
}