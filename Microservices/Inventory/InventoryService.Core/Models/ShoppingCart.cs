using InventoryService.Core.ValueObjects;

namespace InventoryService.Core.Models;

public class ShoppingCart : BaseEntity
{
    public ShoppingCart(int userId, string username)
    {
        UserId = userId;
        CreatedBy = username;
        CreatedDateTime = DateTimeOffset.Now;
        _ShoppingItems = new List<ShoppingItem>();
    }

    public int UserId { get; private set; }
    
    private List<ShoppingItem> _ShoppingItems { get; set; }

    public IReadOnlyCollection<ShoppingItem> ShoppingItems => _ShoppingItems;

    public void AddItem(ShoppingItem shoppingItem, string username)
    {
        var item = _ShoppingItems.FirstOrDefault(x => x.InventoryId == shoppingItem.InventoryId);
        if (item != null) throw new ArgumentException("This item is already in the shopping cart");
        _ShoppingItems.Add(shoppingItem);
        UpdateModifiedFields(username);
    }

    public void UpdateAmountOfItem(Guid inventoryId, int amount, string username)
    {
        var item = _ShoppingItems.FirstOrDefault(x => x.InventoryId == inventoryId);
        if (item != null)
        {
            item.Amount = amount;
            item.UpdatedDateTime = DateTimeOffset.Now;
        }

        UpdateModifiedFields(username);
    }

    public void RemoveItem(ShoppingItem shoppingItem, string username, int amount = 1)
    {
        var item = _ShoppingItems.FirstOrDefault(x => x.InventoryId == shoppingItem.InventoryId);
        if (item != null && item.Amount >= amount)
        {
            item.Amount -= amount;
        }

        UpdateModifiedFields(username);
    }

    public void Delete(string username)
    {
        Delete(username);
    }
}
