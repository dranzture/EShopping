using InvShopRevOrchestrator.Core.Dtos;

namespace InvShopRevOrchestrator.Core.Interfaces;

public interface IInventoryService
{
    Task<Guid> AddInventory(InventoryDto request,  CancellationToken token = default);
    
    Task UpdateInventory(InventoryDto request, string username, CancellationToken token = default);
    
    Task DeleteInventory(InventoryDto request, string username, CancellationToken token = default);
    
    Task IncreaseInventory(InventoryQuantityChangeBaseDto request, string username, CancellationToken token = default);
    
    Task DecreaseInventory(InventoryQuantityChangeBaseDto request, string username, CancellationToken token = default);
    
    Task<InventoryDto?> GetById(Guid id, CancellationToken token = default);
    
    Task<InventoryDto?> GetByName(string name, CancellationToken token = default);
    
    Task<HashSet<InventoryListItemDto>> GetAllInventory(CancellationToken token = default);
}