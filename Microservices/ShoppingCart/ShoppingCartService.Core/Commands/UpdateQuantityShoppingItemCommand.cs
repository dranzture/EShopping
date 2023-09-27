using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.ValueObjects;

namespace ShoppingCartService.Core.Commands;

public class UpdateQuantityShoppingItemCommand : ICommand
{
    private readonly string _username;
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly Guid _shoppingCartId;
    private readonly Inventory _inventory;
    private readonly int _quantity;

    public UpdateQuantityShoppingItemCommand(IShoppingCartRepository shoppingCartRepository, Guid shoppingCartId, 
        Inventory inventory, int quantity, string username)
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
        var shoppingItem = result.ShoppingItems.FirstOrDefault(e => e.InventoryId == _inventory.Id);
        return shoppingItem != null &&  _quantity > 0;
    }

    public async Task Execute()
    {
        var result = await _shoppingCartRepository.GetShoppingCartById(_shoppingCartId);
        result!.UpdateQuantityOfItem(_inventory, _quantity, _username);
        await _shoppingCartRepository.UpdateAsync(result);
        await _shoppingCartRepository.SaveChangesAsync();
    }
}