using InventoryService.Core.Interfaces;

namespace InventoryService.Core.Commands.InventoryCommands;

public class DeleteInventoryCommand : ICommand
{
    private readonly Guid _id;
    private readonly IInventoryRepository _repository;
    private readonly string _username;
    public DeleteInventoryCommand(IInventoryRepository repository, Guid id, string username)
    {
        _repository = repository;
        _id = id;
        _username = username;
    }

    public async Task<bool> CanExecute()
    {
        var item = await _repository.GetById(_id);
        return item != null;
    }

    public async Task Execute()
    {
        var item = await _repository.GetById(_id);
        item.Delete(_username);
        await _repository.UpdateAsync(item);
        await _repository.SaveChangesAsync();
    }
}