using InventoryService.Core.Dtos;
using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;

namespace InventoryService.Core.Services;

public class InventoryService : IInventoryService
{
    public async Task AddInventory(InventoryDto dto, string username, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateInventory(InventoryDto dto, string username, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteInventory(InventoryDto dto, string username, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public async Task IncreaseInventory(InventoryDto dto, int amount, string username, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public async Task DecreaseInventory(InventoryDto dto, int amount, string username, CancellationToken token = default)
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
}