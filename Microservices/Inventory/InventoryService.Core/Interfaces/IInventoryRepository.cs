﻿using InventoryService.Core.Entities;

namespace InventoryService.Core.Interfaces;

public interface IInventoryRepository : IRepository<Inventory>
{
    public Task<HashSet<Inventory>> GetAllInventory(CancellationToken token = default);

    public Task<Inventory?> GetById(Guid id, CancellationToken token = default);

    public Task<Inventory?> GetByName(string name, CancellationToken token = default);
}