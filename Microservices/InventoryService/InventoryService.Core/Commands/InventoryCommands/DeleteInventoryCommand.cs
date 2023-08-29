using InventoryService.Core.Interfaces;

namespace InventoryService.Core.Commands.InventoryCommands;

public class DeleteInventoryCommand : ICommand
{
    public DeleteInventoryCommand()
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