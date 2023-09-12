using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;

namespace InventoryService.Core.Commands.InventoryCommands;

public class AddInventoryCommand : ICommand
{
    private readonly Inventory _item;
    private readonly IInventoryRepository _repository;
    private Inventory? _result = null;

    public AddInventoryCommand(IInventoryRepository repository, Inventory item)
    {
        _repository = repository;
        _item = item;
    }

    public bool CanExecute()
    {
        var item = _repository.GetByName(_item.Name);
        return item == null;
    }

    public void Execute()
    {
        _result = _repository.Create(_item);
    }

    public Inventory? GetResult()
    {
        return _result ?? null;
    }
}