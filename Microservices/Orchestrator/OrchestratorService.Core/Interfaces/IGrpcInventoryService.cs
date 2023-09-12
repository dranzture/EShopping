
using OrchestratorService.Core.Dtos.Inventory;

namespace OrchestratorService.Core.Interfaces;

public interface IGrpcInventoryService
{
    Task<Guid> AddInventory(MutateInventoryDto request, CancellationToken token = default);
    Task UpdateInventory(MutateInventoryDto request, CancellationToken token = default);
    Task DeleteInventory(Guid id, CancellationToken token = default);
    Task IncreaseInventory(ChangeInventoryDto request, CancellationToken token = default);
    Task DecreaseInventory(ChangeInventoryDto request, CancellationToken token = default);
    Task<InventoryDto> GetById(Guid id, CancellationToken token = default);
    Task<InventoryDto> GetByName(string name, CancellationToken token = default); 
}