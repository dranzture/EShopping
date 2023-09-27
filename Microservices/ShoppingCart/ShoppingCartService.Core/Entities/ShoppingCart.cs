using System.ComponentModel.DataAnnotations;
using ShoppingCartService.Core.Extensions;
using ShoppingCartService.Core.Models;
using ShoppingCartService.Core.ValueObjects;

namespace ShoppingCartService.Core.Entities;

public class ShoppingCart : BaseEntity
{
    public ShoppingCart(){} //Ef Required
    
    public ShoppingCart(string username, Guid? id = null)
    {
        Username = username;
        CreatedBy = username;
        CreatedDateTime = DateTimeOffset.Now;
        _shoppingItems = new List<ShoppingItem>();
        if (id.HasValue)
        {
            Id = id.Value;
        }
    }

    [Required] 
    public string Username { get; private set; }

    private readonly List<ShoppingItem> _shoppingItems = new List<ShoppingItem>();

    public IReadOnlyCollection<ShoppingItem> ShoppingItems => _shoppingItems;


    public CheckoutStatus Status { get; private set; } = CheckoutStatus.None;

    public void AddItem(Inventory inventory, int quantity, string username)
    {
        var item = _shoppingItems.FirstOrDefault(x => x.InventoryId == inventory.Id);
        if (item != null) throw new ArgumentException("This item is already in the shopping cart");
        var newItem = new ShoppingItem(inventory.Id, Id, username);
        newItem.UpdateQuantity(quantity, inventory, username);
        _shoppingItems.Add(newItem);
        UpdateModifiedFields(username);
    }

    public void UpdateQuantityOfItem(Inventory inventory, int quantity, string username)
    {
        var item = _shoppingItems.FirstOrDefault(x => x.InventoryId == inventory.Id);
        if (item == null) return;
        item.UpdateQuantity(quantity,inventory, username);
        UpdateModifiedFields(username);
    }

    public void RemoveItem(Inventory inventory, string username)
    {
        var item = _shoppingItems.FirstOrDefault(x => x.InventoryId == inventory.Id);
        if (item == null) return;
        _shoppingItems.Remove(item);
        UpdateModifiedFields(username);
    }

    public void UpdateCheckoutStatus(CheckoutStatus status)
    {
        Status = status;
    }

    public void Delete(string username)
    {
        Delete(username);
    }


}