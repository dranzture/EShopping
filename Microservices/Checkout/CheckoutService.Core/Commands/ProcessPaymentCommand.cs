using CheckoutService.Core.Entities;
using CheckoutService.Core.Interfaces;

namespace CheckoutService.Core.Commands;

public class ProcessPaymentCommand : ICommand
{
    private readonly CreditCard _creditCard;
    private readonly ShoppingCart _shoppingCart;

    public ProcessPaymentCommand(CreditCard creditCard, ShoppingCart shoppingCart)
    {
        _creditCard = creditCard;
        _shoppingCart = shoppingCart;
    }

    public Task<bool> CanExecute()
    {
        return Task.FromResult(_creditCard.CardNumber > 0 && _shoppingCart.ShoppingItems.Sum(e => e.TotalPrice) > 0);
    }

    public Task Execute()
    {
        Console.WriteLine("Processing payment");
        Thread.Sleep(3000);
        Console.WriteLine("Payment processed");
        return Task.CompletedTask;
    }
}