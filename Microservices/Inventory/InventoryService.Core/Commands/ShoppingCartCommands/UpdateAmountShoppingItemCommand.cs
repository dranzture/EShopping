using InventoryService.Core.Interfaces;
using InventoryService.Core.ValueObjects;

namespace InventoryService.Core.Commands.ShoppingCartCommands;

public class UpdateAmountShoppingItemCommand : ICommand
{
    private readonly IShoppingCartRepository _repository;
    private readonly string _username;
    private readonly Guid _cartId;
    private readonly Guid _inventoryId;
    private readonly int _amount;

    public UpdateAmountShoppingItemCommand(IShoppingCartRepository repository, Guid cartId, Guid inventoryId, int amount, string username)
    {
        _inventoryId = inventoryId;
        _amount = amount;
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
        var shoppingItem = result.ShoppingItems.FirstOrDefault(e => e.InventoryId == _inventoryId);
        return shoppingItem != null && shoppingItem.Amount >= _amount;
    }

    public async Task Execute()
    {
        var result = await _repository.GetShoppingCartById(_cartId);
        result.UpdateAmountOfItem(_inventoryId, _amount, _username);
        await _repository.UpdateAsync(result);
        await _repository.SaveChangesAsync();
    }
}