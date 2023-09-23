
using Google.Protobuf.WellKnownTypes;
using InvShopRevOrchestrator.Core.Dtos.Inventory;

namespace InvShopRevOrchestrator.Core.Interfaces;

public interface IGrpcInventoryService
{
    Task<StringValue> AddInventory(UpdateInventoryDto request, CancellationToken token = default);
    
    Task UpdateInventory(UpdateInventoryDto request, CancellationToken token = default);
    
    Task DeleteInventory(InventoryDto id, CancellationToken token = default);
    
    Task IncreaseInventory(InventoryQuantityChangeDto request, CancellationToken token = default);
    
    Task DecreaseInventory(InventoryQuantityChangeDto request, CancellationToken token = default);
    
    Task<InventoryDto> GetById(StringValue id, CancellationToken token = default);
    
    Task<InventoryDto> GetByName(StringValue name, CancellationToken token = default); 
    
    Task<HashSet<InventoryListItemDto>> GetInventoryList(CancellationToken token = default);
}