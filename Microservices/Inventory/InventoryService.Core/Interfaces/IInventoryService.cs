﻿using InventoryService.Core.Dtos;
using InventoryService.Core.Models;

namespace InventoryService.Core.Interfaces;

public interface IInventoryService
{
    Task<string> AddInventory(InventoryDto dto, string username, CancellationToken token = default);
    
    Task UpdateInventory(InventoryDto dto, string username, CancellationToken token = default);

    Task DeleteInventory(Guid id, string username, CancellationToken token = default);
    
    Task IncreaseInventory(Guid id, int amount, string username, CancellationToken token = default);
    
    Task DecreaseInventory(Guid id, int amount, string username, CancellationToken token = default);

    Task<HashSet<Inventory>> GetAllInventory(CancellationToken token = default);

    Task<Inventory?> GetById(Guid id, CancellationToken token = default);
    
    Task<Inventory?> GetByName(string name, CancellationToken token = default);
}