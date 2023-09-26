using InventoryService.Core.Entities;
using InventoryService.Core.Interfaces;

namespace InventoryService.Core.Commands;

public class IncreaseInventoryCommand : ICommand
{
    private readonly IInventoryRepository _repository;
    private readonly Inventory _inventory;
    private readonly int _amount;
    private readonly string _username;
    public IncreaseInventoryCommand(IInventoryRepository repository, Inventory inventory, int amount, string username)
    {
        _repository = repository;
        _inventory = inventory;
        _amount = amount;
        _username = username;
    }
    
    public async Task<bool> CanExecute()
    {
        var item = await _repository.GetById(_inventory.Id);
        return item != null && _amount >= 0;
    }

    public async Task Execute()
    {
        var item = await _repository.GetById(_inventory.Id);
        item!.IncreaseStock(_amount, _username);
        await _repository.UpdateAsync(item);
        await _repository.SaveChangesAsync();
    }    
}