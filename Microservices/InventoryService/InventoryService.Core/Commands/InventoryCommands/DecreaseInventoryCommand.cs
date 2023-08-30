using InventoryService.Core.Interfaces;

namespace InventoryService.Core.Commands.InventoryCommands;
using Models;

public class DecreaseInventoryCommand : ICommand
{
    private readonly IInventoryRepository _repository;
    private readonly Inventory _item;
    private readonly int _amount;
    private readonly string _username;
    public DecreaseInventoryCommand(IInventoryRepository repository, Inventory item, int amount, string username)
    {
        _repository = repository;
        _item = item;
        _amount = amount;
        _username = username;
    }
    
    public async Task<bool> CanExecute()
    {
        var item = await _repository.GetById(_item.Id);
        return item != null;
    }

    public async Task Execute()
    {
        var item = await _repository.GetById(_item.Id);
        item.DecreaseStock(_amount, _username);
        await _repository.Update(item);
    }    
}