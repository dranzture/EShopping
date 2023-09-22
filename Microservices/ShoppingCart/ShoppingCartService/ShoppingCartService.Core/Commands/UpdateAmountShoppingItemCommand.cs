using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.Models;

namespace ShoppingCartService.Core.Commands;

public class UpdateAmountShoppingItemCommand : ICommand
{
    private readonly string _username;
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly ShoppingCart _cart;
    private readonly Inventory _inventory;
    private readonly int _amount;

    public UpdateAmountShoppingItemCommand(IShoppingCartRepository shoppingCartRepository, ShoppingCart cart, 
        Inventory inventory, int amount, string username)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _cart = cart;
        _inventory = inventory;
        _amount = amount;
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
        return shoppingItem != null && shoppingItem.Amount >= _amount;
    }

    public async Task Execute()
    {
        var result = await _shoppingCartRepository.GetShoppingCartById(_cart.Id);
        result!.UpdateAmountOfItem(_inventory, _amount, _username);
        await _shoppingCartRepository.UpdateAsync(result);
        await _shoppingCartRepository.SaveChangesAsync();
    }
}