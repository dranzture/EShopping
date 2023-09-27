using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.ValueObjects;

namespace ShoppingCartService.Core.Commands;

public class AddToShoppingCartCommand : ICommand
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly Guid _shoppingCartId;
    private readonly Inventory _inventory;
    private readonly string _username;
    private readonly int _quantity;

    public AddToShoppingCartCommand(IShoppingCartRepository shoppingCartRepository, 
        Guid shoppingCartId, 
        Inventory inventory, 
        int quantity, 
        string username)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _shoppingCartId = shoppingCartId;
        _inventory = inventory;
        _quantity = quantity;
        _username = username;
    }
    public async Task<bool> CanExecute()
    {
        var result = await _shoppingCartRepository.GetShoppingCartById(_shoppingCartId);
        if (result is not { Status: CheckoutStatus.None })
        {
            return false;
        }

        if (result.ShoppingItems == null || result.ShoppingItems.Count == 0) return true;
        var shoppingItem = result.ShoppingItems.FirstOrDefault(e => e.InventoryId == _inventory.Id);
        return shoppingItem == null;
    }

    public async Task Execute()
    {
        var result = await _shoppingCartRepository.GetShoppingCartById(_shoppingCartId);
        result!.AddItem(_inventory, _quantity,_username);
        await _shoppingCartRepository.UpdateAsync(result);
        await _shoppingCartRepository.SaveChangesAsync();
    }
}
