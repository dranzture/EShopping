using OrchestratorService.Core.Dtos.Inventory;
using OrchestratorService.Core.Interfaces;

namespace OrchestratorService.API.SyncDataServices;

public class GrpcInventoryService : IGrpcInventoryService
{
    public Task AddInventory(MutateInventoryDto request, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateInventory(MutateInventoryDto request, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteInventory(InventoryDto request, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task IncreaseInventory(ChangeInventoryDto request, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task DecreaseInventory(ChangeInventoryDto request, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<InventoryDto> GetById(Guid id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<InventoryDto> GetByName(string name, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}