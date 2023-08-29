using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;

namespace InventoryService.Core.Commands.InventoryCommands;

public class UpdateInventoryCommand : ICommand
{
    public UpdateInventoryCommand(IInventoryRepository repository)
    {
        
    }
    
    public async Task<bool> CanExecute()
    {
        throw new NotImplementedException();
    }

    public async Task Execute()
    {
        throw new NotImplementedException();
    }
}