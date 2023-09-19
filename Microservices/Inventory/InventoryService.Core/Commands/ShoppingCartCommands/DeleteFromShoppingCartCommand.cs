using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;
using InventoryService.Core.ValueObjects;

namespace InventoryService.Core.Commands.ShoppingCartCommands;

public class DeleteFromShoppingCartCommand : ICommand
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly ShoppingCart _cart;
    private readonly string _username;
    private readonly Inventory _inventory;

    public DeleteFromShoppingCartCommand(IShoppingCartRepository shoppingCartRepository, 
        IInventoryRepository inventoryRepository, 
        ShoppingCart cart, 
        Inventory inventory, 
        string username)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _inventoryRepository = inventoryRepository;
        _cart = cart;
        _inventory = inventory;
        _username = username;
    }
    public async Task<bool> CanExecute()
    {
        var result = await _shoppingCartRepository.GetShoppingCartById(_cart.Id);
        var inventory = await _inventoryRepository.GetById(_inventory.Id);
        if (result is not { Status: ShoppingCart.CheckoutStatus.None } || inventory is null)
        {
            return false;
        }
        var shoppingItem = result.ShoppingItems.FirstOrDefault(e => e.Item.Id == _inventory.Id);
        return shoppingItem != null;
    }

    public async Task Execute()
    {
        var result = await _shoppingCartRepository.GetShoppingCartById(_cart.Id);
        var inventory = await _inventoryRepository.GetById(_inventory.Id);
        result!.RemoveItem(inventory!, _username);
        await _shoppingCartRepository.UpdateAsync(result);
        await _shoppingCartRepository.SaveChangesAsync();
    }
}