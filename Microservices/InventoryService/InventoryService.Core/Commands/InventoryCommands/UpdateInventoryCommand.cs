using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;

namespace InventoryService.Core.Commands.InventoryCommands;

public class UpdateInventoryCommand : ICommand
{
    private readonly Inventory _item;
    private readonly IInventoryRepository _repository;
    private readonly string _username;
    
    public UpdateInventoryCommand(IInventoryRepository repository, Inventory item, string username)
    {
        _repository = repository;
        _item = item;
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
        item.ChangeDescription(_item.Description, _username);
        item.ChangeSize(_item.Height,_item.Width, _username);
        await _repository.Update(item);
        await _repository.SaveChanges();
    }
}