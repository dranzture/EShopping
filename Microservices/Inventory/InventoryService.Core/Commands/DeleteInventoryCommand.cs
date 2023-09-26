using InventoryService.Core.Entities;
using InventoryService.Core.Interfaces;

namespace InventoryService.Core.Commands;

public class DeleteInventoryCommand : ICommand
{
    private readonly IInventoryRepository _repository;
    private readonly Inventory _inventory;
    private readonly string _username;
    public DeleteInventoryCommand(IInventoryRepository repository, Inventory inventory, string username)
    {
        _repository = repository;
        _inventory = inventory;
        _username = username;
    }

    public async Task<bool> CanExecute()
    {
        var item = await _repository.GetById(_inventory.Id);
        return item != null;
    }

    public async Task Execute()
    {
        var item = await _repository.GetById(_inventory.Id);
        item!.Delete(_username);
        await _repository.UpdateAsync(item);
        await _repository.SaveChangesAsync();
    }
}