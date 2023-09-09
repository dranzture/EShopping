using InventoryService.Core.Interfaces;

namespace InventoryService.Core.Commands.InventoryCommands;

using Models;

public class DecreaseInventoryCommand : ICommand
{
    private readonly IInventoryRepository _repository;
    private readonly Guid _id;
    private readonly int _amount;
    private readonly string _username;

    public DecreaseInventoryCommand(IInventoryRepository repository, Guid id, int amount, string username)
    {
        _repository = repository;
        _id = id;
        _amount = amount;
        _username = username;
    }

    public async Task<bool> CanExecute()
    {
        var item = await _repository.GetById(_id);
        return item != null && _amount >= 0 && item.InStock >= _amount;
    }

    public async Task Execute()
    {
        var item = await _repository.GetById(_id);
        item.DecreaseStock(_amount, _username);
        await _repository.Update(item);
    }
}