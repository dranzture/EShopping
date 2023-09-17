using InventoryService.Core.Interfaces;
using InventoryService.Core.ValueObjects;

namespace InventoryService.Core.Commands.ShoppingCartCommands;

public class DeleteFromShoppingCartCommand : ICommand
{
    private readonly IShoppingCartRepository _repository;
    private readonly ShoppingItem _shoppingItem;
    private readonly string _username;
    private readonly Guid _cartId;
    
    public DeleteFromShoppingCartCommand(IShoppingCartRepository repository, Guid cartId, ShoppingItem shoppingItem, string username)
    {
        _shoppingItem = shoppingItem;
        _repository = repository;
        _cartId = cartId;
        _username = username;
    }
    public async Task<bool> CanExecute()
    {
        var result = await _repository.GetShoppingCartById(_cartId);
        if (result == null)
        {
            return false;
        }
        var shoppingItem = result.ShoppingItems.Where(e => e.InventoryId == _shoppingItem.InventoryId);
        return shoppingItem != null;
    }

    public async Task Execute()
    {
        var result = await _repository.GetShoppingCartById(_cartId);
        result.RemoveItem(_shoppingItem, _username);
        await _repository.UpdateAsync(result);
        await _repository.SaveChangesAsync();
    }
}