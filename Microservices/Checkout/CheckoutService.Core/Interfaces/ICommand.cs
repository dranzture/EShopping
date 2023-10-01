namespace CheckoutService.Core.Interfaces;

public interface ICommand
{
    Task<bool> CanExecute();
    
    Task Execute();
}