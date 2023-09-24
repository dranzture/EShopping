
using Google.Protobuf.WellKnownTypes;
using InvShopRevOrchestrator.Core.Dtos;

namespace InvShopRevOrchestrator.Core.Interfaces;

public interface IGrpcInventoryService
{
    Task<StringValue> AddInventory(InventoryDto request, CancellationToken token = default);
    
    Task UpdateInventory(InventoryWithUsernameDto request, CancellationToken token = default);
    
    Task DeleteInventory(InventoryWithUsernameDto id, CancellationToken token = default);
    
    Task IncreaseInventory(InventoryQuantityChangeRequestDto request, CancellationToken token = default);
    
    Task DecreaseInventory(InventoryQuantityChangeRequestDto request, CancellationToken token = default);
    
    Task<InventoryDto> GetById(Guid id, CancellationToken token = default);
    
    Task<InventoryDto> GetByName(string name, CancellationToken token = default); 
    
    Task<HashSet<InventoryListItemDto>> GetInventoryList(CancellationToken token = default);
}