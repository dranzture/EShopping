using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.Models;

namespace ShoppingCartService.Core.Commands;

public class DeleteFromShoppingCartCommand : ICommand
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly ShoppingCart _cart;
    private readonly string _username;
    private readonly Inventory _inventory;

    public DeleteFromShoppingCartCommand(IShoppingCartRepository shoppingCartRepository, 
        ShoppingCart cart, 
        Inventory inventory, 
        string username)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _cart = cart;
        _inventory = inventory;
        _username = username;
    }
    public async Task<bool> CanExecute()
    {
        var result = await _shoppingCartRepository.GetShoppingCartById(_cart.Id);
        if (result is not { Status: ShoppingCart.CheckoutStatus.None })
        {
            return false;
        }
        var shoppingItem = result.ShoppingItems.FirstOrDefault(e => e.Item.Id == _inventory.Id);
        return shoppingItem != null;
    }

    public async Task Execute()
    {
        var result = await _shoppingCartRepository.GetShoppingCartById(_cart.Id);
        result!.RemoveItem(_inventory!, _username);
        await _shoppingCartRepository.UpdateAsync(result);
        await _shoppingCartRepository.SaveChangesAsync();
    }
}