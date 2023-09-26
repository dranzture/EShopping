using InvShopRevOrchestrator.Core.Dtos;
using InvShopRevOrchestrator.Core.Interfaces;

namespace InvShopRevOrchestrator.Core.Services;

public class InventoryService : IInventoryService
{
    private readonly IGrpcInventoryService _grpcInventoryService;

    public InventoryService(IGrpcInventoryService grpcInventoryService)
    {
        _grpcInventoryService = grpcInventoryService;
    }

    public async Task<Guid> AddInventory(InventoryDto request, string username, CancellationToken token = default)
    {
        try
        {
            var inventoryWithUsernameDto = new InventoryWithUsernameDto
            {
                Dto = request,
                Username = username
            };
            var inventoryId = await _grpcInventoryService.AddInventory(inventoryWithUsernameDto, token);
            return new Guid(inventoryId.Value);
        }
        catch
        {
            throw;
        }
    }

    public async Task UpdateInventory(InventoryDto request, string username, CancellationToken token = default)
    {
        try
        {
            var inventoryWithUsernameDto = new InventoryWithUsernameDto
            {
                Dto = request,
                Username = username
            };
            await _grpcInventoryService.UpdateInventory(inventoryWithUsernameDto, token);
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteInventory(InventoryDto request, string username, CancellationToken token = default)
    {
        try
        {
            var inventoryWithUsernameDto = new InventoryWithUsernameDto
            {
                Dto = request,
                Username = username
            };
            await _grpcInventoryService.DeleteInventory(inventoryWithUsernameDto, token);
        }
        catch
        {
            throw;
        }
    }


    public async Task IncreaseInventory(InventoryQuantityChangeBaseDto request, string username,
        CancellationToken token = default)
    {
        try
        {
            var inventoryQuantityChangeDto = new InventoryQuantityChangeRequestDto(request.Dto, request.Amount, username);
            await _grpcInventoryService.IncreaseInventory(inventoryQuantityChangeDto, token);
        }
        catch
        {
            throw;
        }
    }

    public async Task DecreaseInventory(InventoryQuantityChangeBaseDto request, string username,
        CancellationToken token = default)
    {
        try
        {
            var inventoryQuantityChangeDto = new InventoryQuantityChangeRequestDto(request.Dto, request.Amount, username);
            await _grpcInventoryService.DecreaseInventory(inventoryQuantityChangeDto, token);
        }
        catch
        {
            throw;
        }
    }

    public async Task<InventoryDto?> GetById(Guid id, CancellationToken token = default)
    {
        try
        {
            var inventory = await _grpcInventoryService.GetById(id, token);
            return inventory;
        }
        catch
        {
            throw;
        }
    }

    public async Task<InventoryDto?> GetByName(string name, CancellationToken token = default)
    {
        try
        {
            var inventory = await _grpcInventoryService.GetByName(name, token);
            return inventory;
        }
        catch
        {
            throw;
        }
    }

    public async Task<HashSet<InventoryListItemDto>> GetAllInventory(CancellationToken token = default)
    {
        try
        {
            var inventories = await _grpcInventoryService.GetInventoryList(token);
            return inventories;
        }
        catch
        {
            throw;
        }
    }
}