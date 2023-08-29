using InventoryService.Core.Interfaces;

namespace InventoryService.Core.Commands.InventoryCommands;
using Models;

public class DecreaseInventoryCommand : ICommand
{
    private readonly IInventoryRepository _repository;
    private readonly Inventory _item;
    private readonly int _amount;
    
    public DecreaseInventoryCommand(IInventoryRepository repository, Inventory item, int amount)
    {
        _repository = repository;
        _item = item;
        _amount = amount;
    }
    
    public Task<bool> CanExecute()
    {
        return Task.FromResult(_item.InStock >= _amount);
    }

    public async Task Execute()
    {
        try
        {
            await _repository.DecreaseInventoryById(_item.Id, _amount);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not decrease inventory {_item.Description} due to: {ex.Message}");
        }
    }
}