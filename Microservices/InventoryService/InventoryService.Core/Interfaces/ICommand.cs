namespace InventoryService.Core.Interfaces;

public interface ICommand
{
    Task<bool>  CanExecute();
    Task Execute();
}