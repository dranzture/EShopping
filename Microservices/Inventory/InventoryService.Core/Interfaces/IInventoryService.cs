using InventoryService.Core.Dtos;
using InventoryService.Core.Models;

namespace InventoryService.Core.Interfaces;

public interface IInventoryService
{
    string AddInventory(InventoryDto dto, string username, CancellationToken token = default);
    
    void UpdateInventory(InventoryDto dto, string username, CancellationToken token = default);

    void DeleteInventory(Guid id, CancellationToken token = default);
    
    void IncreaseInventory(Guid id, int amount, string username, CancellationToken token = default);
    
    void DecreaseInventory(Guid id, int amount, string username, CancellationToken token = default);

    HashSet<Inventory> GetAllInventory(CancellationToken token = default);

    Inventory? GetById(Guid id, CancellationToken token = default);
    
    Inventory? GetByName(string name, CancellationToken token = default);
}