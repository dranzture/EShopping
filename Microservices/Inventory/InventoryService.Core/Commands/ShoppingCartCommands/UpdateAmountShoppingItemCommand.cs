using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;
using InventoryService.Core.ValueObjects;

namespace InventoryService.Core.Commands.ShoppingCartCommands;

public class UpdateAmountShoppingItemCommand : ICommand
{
    private readonly string _username;
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly ShoppingCart _cart;
    private readonly Inventory _inventory;
    private readonly int _amount;

    public UpdateAmountShoppingItemCommand(IShoppingCartRepository shoppingCartRepository, IInventoryRepository inventoryRepository, ShoppingCart cart, 
        Inventory inventory, int amount, string username)
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
        var result = await _shoppingCartRepository.GetShoppingCartById(_cart.Id);
        var inventory = await _inventoryRepository.GetById(_inventory.Id);
        if (result is not { Status: ShoppingCart.CheckoutStatus.None } || inventory is null)
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