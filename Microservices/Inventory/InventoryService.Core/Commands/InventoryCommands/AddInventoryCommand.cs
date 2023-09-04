using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;

namespace InventoryService.Core.Commands.InventoryCommands;

public class AddInventoryCommand : ICommand
{
    private readonly Inventory _item;
    private readonly IInventoryRepository _repository;

    public AddInventoryCommand(IInventoryRepository repository, Inventory item)
    {
        _repository = repository;
        _item = item;
    }
    
    public async Task<bool> CanExecute()
    {
        var item = await _repository.GetByName(_item.Name);
        return item == null;
    }

    public async Task Execute()
    {
        await _repository.Create(_item);
    }
}