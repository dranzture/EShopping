using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;

namespace InventoryService.Core.Services;

public class InventoryRepository : IInventoryRepository
{
    public async Task<bool> Create(Inventory item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Update(Inventory item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Delete(Inventory item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SaveChanges(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<HashSet<Inventory>> GetAllInventory(CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Inventory> GetById(Guid id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Inventory> GetByName(string name, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public async Task IncreaseInventoryById(Guid id, int amount, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public async Task DecreaseInventoryById(Guid id, int amount, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}