using InventoryService.Core.Interfaces;
using InventoryService.Core.ValueObjects;

namespace InventoryService.Core.Commands.ShoppingCartCommands;

public class DeleteShoppingCart : ICommand
{
    private readonly IShoppingCartRepository _repository;
    private readonly ShoppingItem _shoppingItem;
    private readonly string _username;
    private readonly Guid _cartId;

    public DeleteShoppingCart(IShoppingCartRepository repository, Guid cartId, ShoppingItem shoppingItem, string username)
    {
        _shoppingItem = shoppingItem;
        _repository = repository;
        _username = username;
        _cartId = cartId;
    }
    public async Task<bool> CanExecute()
    {
        var result = await _repository.GetShoppingCartById(_cartId);
        return result != null;
    }

    public async Task Execute()
    {
        var result = await _repository.GetShoppingCartById(_cartId);
        result.Delete( _username);
        await _repository.UpdateAsync(result);
        await _repository.SaveChangesAsync();
    }
}