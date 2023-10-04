namespace ShippingService.Core.Interfaces;

public interface IRepository<T> where  T : class
{
    public IQueryable<T> Queryable(CancellationToken cancellationToken = default);
    
    public Task AddAsync(T item, CancellationToken cancellationToken = default);
    
    public Task UpdateAsync(T item, CancellationToken cancellationToken = default);
    
    public Task<bool> SaveChanges(CancellationToken cancellationToken = default);
}