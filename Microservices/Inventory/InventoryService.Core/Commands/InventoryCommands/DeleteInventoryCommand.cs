using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;

namespace InventoryService.Core.Commands.InventoryCommands;

public class DeleteInventoryCommand : ICommand
{
    private readonly Inventory _item;
    private readonly IInventoryRepository _repository;

    public DeleteInventoryCommand(IInventoryRepository repository, Inventory item)
    {
        _repository = repository;
        _item = item;
    }
    public async Task<bool> CanExecute()
    {
        var item = await _repository.GetById(_item.Id);
        return item != null;
    }

    public async Task Execute()
    {
        var item = await _repository.GetById(_item.Id);
        await _repository.Delete(item);
    }
}