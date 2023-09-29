using System.ComponentModel.DataAnnotations;
using ShoppingCartService.Core.Dtos;
using ShoppingCartService.Core.Extensions;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.Models;
using ShoppingCartService.Core.Notifications;
using ShoppingCartService.Core.ValueObjects;

namespace ShoppingCartService.Core.Entities;

public class ShoppingCart : BaseEntity
{
    public ShoppingCart()
    {
    } //Ef Required

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

        AddDomainEvent(new ItemAddedToShoppingCartEvent(new ChangeInventoryQuantityDto()
            { InventoryId = inventory.Id, Quantity = quantity }));

        UpdateModifiedFields(username);
    }

    public void UpdateQuantityOfItem(Inventory inventory, int quantity, string username)
    {
        var item = _shoppingItems.FirstOrDefault(x => x.InventoryId == inventory.Id);

        if (item == null) return;

        if (item.Quantity >= quantity)
        {
            AddDomainEvent(new ItemRemovedFromShoppingCartEvent(new ChangeInventoryQuantityDto()
                { InventoryId = inventory.Id, Quantity = item.Quantity - quantity }));
        }
        else
        {
            AddDomainEvent(new ItemAddedToShoppingCartEvent(new ChangeInventoryQuantityDto()
                { InventoryId = inventory.Id, Quantity = quantity - item.Quantity }));
        }

        item.UpdateQuantity(quantity, inventory, username);

        item.UpdateModifiedFields(username);
        UpdateModifiedFields(username);
    }

    public void RemoveItem(Inventory inventory, string username)
    {
        var item = _shoppingItems.FirstOrDefault(x => x.InventoryId == inventory.Id);
        if (item == null) return;

        AddDomainEvent(new ItemRemovedFromShoppingCartEvent(new ChangeInventoryQuantityDto()
            { InventoryId = inventory.Id, Quantity = item.Quantity }));

        _shoppingItems.Remove(item);
    }

    public void UpdateCheckoutStatus(CheckoutStatus status)
    {
        Status = status;
    }
}