﻿using InventoryService.Core.Dtos;
using InventoryService.Core.Models;

namespace InventoryService.Core.Interfaces;

public interface IInventoryService
{
    Task AddInventory(InventoryDto dto, string username, CancellationToken token = default);
    
    Task UpdateInventory(InventoryDto dto, string username, CancellationToken token = default);

    Task DeleteInventory(InventoryDto dto, CancellationToken token = default);
    
    Task IncreaseInventory(InventoryDto dto, int amount, string username, CancellationToken token = default);
    
    Task DecreaseInventory(InventoryDto dto, int amount, string username, CancellationToken token = default);

    Task<HashSet<Inventory>> GetAllInventory(CancellationToken token = default);

    Task<Inventory> GetById(Guid id, CancellationToken token = default);
    
    Task<Inventory> GetByName(string name, CancellationToken token = default);
}