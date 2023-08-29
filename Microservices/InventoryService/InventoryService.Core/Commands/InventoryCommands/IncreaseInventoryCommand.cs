using InventoryService.Core.Interfaces;

namespace InventoryService.Core.Commands.InventoryCommands;
using Models;

public class IncreaseInventoryCommand : ICommand
{
    private readonly IInventoryRepository _repository;
    private readonly Inventory _item;
    private readonly int _amount;
    
    public IncreaseInventoryCommand(IInventoryRepository repository, Inventory item, int amount)
    {
        _repository = repository;
        _item = item;
        _amount = amount;
    }
    
    public Task<bool> CanExecute()
    {
        return Task.FromResult(true);
    }

    public async Task Execute()
    {
        try
        {
            _repository.IncreaseInventoryById(_item.Id, _amount);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not increase inventory {_item.Description} due to: {ex.Message}");
        }
    }    
}