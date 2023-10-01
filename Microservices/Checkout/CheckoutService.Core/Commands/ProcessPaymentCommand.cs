using CheckoutService.Core.Entities;
using CheckoutService.Core.Interfaces;
using CheckoutService.Core.ValueObjects;

namespace CheckoutService.Core.Commands;

public class ProcessPaymentCommand : ICommand
{
    public ProcessPaymentCommand(CreditCard creditCard, ShoppingCart shoppingCart)
    {
        
    }
    public Task<bool> CanExecute()
    {
        throw new NotImplementedException();
    }

    public Task Execute()
    {
        throw new NotImplementedException();
    }
}