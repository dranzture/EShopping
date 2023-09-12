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

    public bool CanExecute()
    {
        var item = _repository.GetById(_id);
        return item != null;
    }

    public void Execute()
    {
        var item = _repository.GetById(_id);
        _repository.Delete(item);
    }
}