using Microsoft.EntityFrameworkCore;

namespace InventoryService.Core.Interfaces;

public interface IRepository<T> where  T : class
{
    public IQueryable<T> Queryable(CancellationToken cancellationToken = default);
    
    public T Create(T item, CancellationToken cancellationToken = default);
    
    public void Update(T item, CancellationToken cancellationToken = default);
    
    public void Delete(T item, CancellationToken cancellationToken = default);

    public bool SaveChanges();
}