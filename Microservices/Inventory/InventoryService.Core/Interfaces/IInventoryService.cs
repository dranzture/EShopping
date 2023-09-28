using InventoryService.Core.Dtos;
using InventoryService.Core.Entities;

namespace InventoryService.Core.Interfaces;

public interface IInventoryService
{
    Task<string> AddInventory(InventoryDto dto, string username, CancellationToken token = default);
    
    Task UpdateInventory(InventoryDto dto, string username, CancellationToken token = default);

    Task DeleteInventory(InventoryDto dto, string username, CancellationToken token = default);
    
    Task IncreaseInventory(ChangeInventoryQuantityDto dto, CancellationToken token = default);
    
    Task DecreaseInventory(ChangeInventoryQuantityDto dto, CancellationToken token = default);

    Task<HashSet<Inventory>> GetAllInventory(CancellationToken token = default);

    Task<Inventory?> GetById(Guid id, CancellationToken token = default);
    
    Task<Inventory?> GetByName(string name, CancellationToken token = default);
}