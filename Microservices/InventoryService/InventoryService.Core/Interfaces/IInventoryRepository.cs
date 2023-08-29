using InventoryService.Core.Models;

namespace InventoryService.Core.Interfaces;

public interface IInventoryRepository : IRepositoryBase<Inventory>
{
    public Task<HashSet<Inventory>> GetAllInventory(CancellationToken token = default);

    public Task<Inventory> GetById(Guid id, CancellationToken token = default);
    
    public Task<Inventory> GetByName(string name, CancellationToken token = default);

    public Task IncreaseInventoryById(Guid id, int amount, CancellationToken token = default);

    public Task DecreaseInventoryById(Guid id, int amount, CancellationToken token = default);
}