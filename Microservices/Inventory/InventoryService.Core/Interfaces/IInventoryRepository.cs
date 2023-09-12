using InventoryService.Core.Models;

namespace InventoryService.Core.Interfaces;

public interface IInventoryRepository : IRepository<Inventory>
{
    public HashSet<Inventory> GetAllInventory(CancellationToken token = default);

    public Inventory? GetById(Guid id, CancellationToken token = default);

    public Inventory? GetByName(string name, CancellationToken token = default);
}