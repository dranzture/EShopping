using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.ValueObjects;

namespace ShoppingCartService.Core.Commands;

public class UpdateQuantityShoppingItemCommand : ICommand
{
    private readonly string _username;
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly ShoppingCart _cart;
    private readonly Inventory _inventory;
    private readonly int _quantity;

    public UpdateQuantityShoppingItemCommand(IShoppingCartRepository shoppingCartRepository, ShoppingCart cart, 
        Inventory inventory, int quantity, string username)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _cart = cart;
        _inventory = inventory;
        _quantity = quantity;
        _username = username;
    }
    
    public async Task<bool> CanExecute()
    {
        var result = await _shoppingCartRepository.GetShoppingCartById(_cart.Id);
        if (result is not { Status: CheckoutStatus.None })
        {
            return false;
        }
        var shoppingItem = result.ShoppingItems.FirstOrDefault(e => e.Item.Id == _inventory.Id);
        return shoppingItem != null &&  _quantity > 0;
    }

    public async Task Execute()
    {
        var result = await _shoppingCartRepository.GetShoppingCartById(_cart.Id);
        result!.UpdateQuantityOfItem(_inventory, _quantity, _username);
        await _shoppingCartRepository.UpdateAsync(result);
        await _shoppingCartRepository.SaveChangesAsync();
    }
}