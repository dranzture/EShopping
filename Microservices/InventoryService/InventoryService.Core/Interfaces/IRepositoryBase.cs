using InventoryService.Core.Models;

namespace InventoryService.Core.Interfaces;

public interface IRepositoryBase<in T> where T : class
{
    public Task<bool> Create(T item, CancellationToken cancellationToken = default);
    
    public Task<bool> Update(T item, CancellationToken cancellationToken = default);
    
    public Task<bool> Delete(T item, CancellationToken cancellationToken = default);

    public Task<bool> SaveChanges(CancellationToken cancellationToken = default);
}