using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.ValueObjects;

namespace ShoppingCartService.Core.Commands;

public class DeleteFromShoppingCartCommand : ICommand
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly Guid _shoppingCartId;
    private readonly string _username;
    private readonly Inventory _inventory;

    public DeleteFromShoppingCartCommand(IShoppingCartRepository shoppingCartRepository, 
        Guid shoppingCartId, 
        Inventory inventory, 
        string username)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _shoppingCartId = shoppingCartId;
        _inventory = inventory;
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
        return shoppingItem != null;
    }

    public async Task Execute()
    {
        var result = await _shoppingCartRepository.GetShoppingCartById(_shoppingCartId);
        result!.RemoveItem(_inventory!, _username);
        await _shoppingCartRepository.UpdateAsync(result);
        await _shoppingCartRepository.SaveChangesAsync();
    }
}