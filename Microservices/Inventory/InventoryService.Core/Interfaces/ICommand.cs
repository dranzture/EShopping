namespace InventoryService.Core.Interfaces;

public interface ICommand
{
    bool CanExecute();
    void Execute();
}