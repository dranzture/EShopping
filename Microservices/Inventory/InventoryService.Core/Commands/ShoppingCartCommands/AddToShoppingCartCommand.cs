using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;
using InventoryService.Core.ValueObjects;

namespace InventoryService.Core.Commands.ShoppingCartCommands;

public class AddToShoppingCartCommand : ICommand
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly ShoppingCart _cart;
    private readonly Inventory _inventory;
    private readonly string _username;
    private readonly int _amount;

    public AddToShoppingCartCommand(IShoppingCartRepository shoppingCartRepository, 
        IInventoryRepository inventoryRepository, 
        ShoppingCart cart, 
        Inventory inventory, 
        int amount, 
        string username)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _inventoryRepository = inventoryRepository;
        _cart = cart;
        _inventory = inventory;
        _amount = amount;
        _username = username;
    }
    public async Task<bool> CanExecute()
    {
        var result = await _shoppingCartRepository.GetShoppingCartByUsername(_username);
        if (result is not { Status: ShoppingCart.CheckoutStatus.None })
        {
            return false;
        }
        var shoppingItem = result.ShoppingItems.FirstOrDefault(e => e.Item.Id == _inventory.Id);
        return shoppingItem == null;
    }

    public async Task Execute()
    {
        var result = await _shoppingCartRepository.GetShoppingCartById(_cart.Id);
        result!.AddItem(_inventory, _amount,_username);
        await _shoppingCartRepository.UpdateAsync(result);
        await _shoppingCartRepository.SaveChangesAsync();
    }
}