using InvShopRevOrchestrator.Core.Dtos.Inventory;

namespace InvShopRevOrchestrator.Core.Interfaces;

public interface IInventoryService
{
    Task<Guid> AddInventory(InventoryDto request, CancellationToken token = default);
    Task UpdateInventory(InventoryDto request, CancellationToken token = default);
    Task DeleteInventory(InventoryDto request, CancellationToken token = default);
    Task<HashSet<InventoryDto>> GetAllInventory(CancellationToken token = default);
}