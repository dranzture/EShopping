using InventoryService.Core.Entities;
using InventoryService.Core.Interfaces;

namespace InventoryService.Core.Commands;

public class AddInventoryCommand : ICommand
{
    private readonly Inventory _item;
    private readonly string _username;
    private readonly IInventoryRepository _repository;
    private Inventory? _result = null;

    public AddInventoryCommand(IInventoryRepository repository, Inventory item, string username)
    {
        _repository = repository;
        _item = item;
        _username = username;
    }

    public async Task<bool> CanExecute()
    {
        var item = await _repository.GetByName(_item.Name);
        return item == null;
    }

    public async Task Execute()
    {
        _item.UpdateCreatedFields(_username);
        await _repository.AddAsync(_item);
        await _repository.SaveChangesAsync();
        _result = _item;
    }

    public Inventory? GetResult()
    {
        return _result ?? null;
    }
}