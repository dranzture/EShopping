namespace ShoppingCartService.Core.Interfaces;

public interface IRepository<T> where  T : class
{
    public Task<IQueryable<T>> Queryable(CancellationToken cancellationToken = default);
    
    public Task AddAsync(T item, CancellationToken cancellationToken = default);
    
    public Task UpdateAsync(T item, CancellationToken cancellationToken = default);
    
    public Task DeleteAsync(T item, CancellationToken cancellationToken = default);

    public Task<bool> SaveChangesAsync();
}