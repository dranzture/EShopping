using InventoryService.Core.Interfaces;

namespace InventoryService.Core.Commands.InventoryCommands;

public class IncreaseInventoryCommand : ICommand
{
    private readonly IInventoryRepository _repository;
    private readonly Guid _id;
    private readonly int _amount;
    private readonly string _username;
    public IncreaseInventoryCommand(IInventoryRepository repository, Guid id, int amount, string username)
    {
        _repository = repository;
        _id = id;
        _amount = amount;
        _username = username;
    }
    
    public async Task<bool> CanExecute()
    {
        var item = await _repository.GetById(_id);
        return item != null && _amount >= 0;
    }

    public async Task Execute()
    {
        var item = await _repository.GetById(_id);
        item.IncreaseStock(_amount, _username);
        await _repository.UpdateAsync(item);
        await _repository.SaveChangesAsync();
    }    
}