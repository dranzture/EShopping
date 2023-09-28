﻿using InventoryService.Core.Entities;
using InventoryService.Core.Interfaces;

namespace InventoryService.Core.Commands;

public class DecreaseInventoryCommand : ICommand
{
    private readonly IInventoryRepository _repository;
    private readonly Guid _id;
    private readonly int _amount;

    public DecreaseInventoryCommand(IInventoryRepository repository, Guid id, int amount)
    {
        _repository = repository;
        _id = id;
        _amount = amount;
    }

    public async Task<bool> CanExecute()
    {
        var item = await _repository.GetById(_id);
        return item != null && _amount >= 0 && item.InStock >= _amount;
    }

    public async Task Execute()
    {
        var item = await _repository.GetById(_id);
        item!.DecreaseStock(_amount);
        await _repository.UpdateAsync(item);
        await _repository.SaveChangesAsync();
    }
}