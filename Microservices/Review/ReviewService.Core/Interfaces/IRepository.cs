namespace ReviewService.Core.Interfaces;

public interface IRepository<T> where  T : class
{
    public IQueryable<T> Queryable(CancellationToken cancellationToken = default);
    
    public Task Create(T item, CancellationToken cancellationToken = default);
    
    public Task Update(T item, CancellationToken cancellationToken = default);
    
    public Task Delete(T item, CancellationToken cancellationToken = default);

    public Task<bool> SaveChanges(CancellationToken cancellationToken = default);
}