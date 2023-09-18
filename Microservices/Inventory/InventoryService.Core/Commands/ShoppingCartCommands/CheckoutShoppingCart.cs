using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;

namespace InventoryService.Core.Commands.ShoppingCartCommands;

public class CheckoutShoppingCart : ICommand
{
    private readonly IShoppingCartRepository _repository;
    private readonly ShoppingCart _cart;

    public CheckoutShoppingCart(IShoppingCartRepository repository, ShoppingCart cart)
    {
        _repository = repository;
        _cart = cart;
    }

    public async Task<bool> CanExecute()
    {
        var result = await _repository.GetShoppingCartById(_cart.Id);
        if (result == null) return false;
        return result.ShoppingItems.Count > 0;
    }

    public async Task Execute()
    {
        var orderId = new Guid();
        
    }
}