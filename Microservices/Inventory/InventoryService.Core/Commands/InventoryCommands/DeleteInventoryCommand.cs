using InventoryService.Core.Interfaces;

namespace InventoryService.Core.Commands.InventoryCommands;

public class DeleteInventoryCommand : ICommand
{
    private readonly Guid _id;
    private readonly IInventoryRepository _repository;

    public DeleteInventoryCommand(IInventoryRepository repository, Guid id)
    {
        _repository = repository;
        _id = id;
    }

    public async Task<bool> CanExecute()
    {
        var item = await _repository.GetById(_id);
        return item != null;
    }

    public async Task Execute()
    {
        var item = await _repository.GetById(_id);
        await _repository.DeleteAsync(item);
        await _repository.SaveChangesAsync();
    }
}