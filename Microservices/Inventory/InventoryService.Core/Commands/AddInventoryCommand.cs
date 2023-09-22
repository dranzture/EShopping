using InventoryService.Core.Entities;
using InventoryService.Core.Interfaces;

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

    public async Task<bool> CanExecute()
    {
        var item = await _repository.GetByName(_item.Name);
        return item == null;
    }

    public async Task Execute()
    {
        await _repository.AddAsync(_item);
        await _repository.SaveChangesAsync();
        _result = _item;
    }

    public Inventory? GetResult()
    {
        return _result ?? null;
    }
}