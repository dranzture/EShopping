using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.Models;

namespace ShoppingCartService.Core.ValueObjects;

public record ShoppingItem
{
    public ShoppingItem(Inventory item, int quantity, decimal totalPrice, Guid shoppingCartId)
    {
        Item = item;
        Quantity = quantity;
        TotalPrice = totalPrice;
        _shoppingCartId = shoppingCartId;
        ItemAdded();
    } 
    
    
    private readonly Guid _shoppingCartId;
    
    public Inventory Item { get; private set; }

    public int Quantity { get; private set; }

    public decimal TotalPrice { get; private set; }

    public DateTimeOffset AddedDateTime { get; private  set; }

    public DateTimeOffset? UpdatedDateTime { get; private set; }

    private void ItemUpdated()
    {
        UpdatedDateTime = DateTimeOffset.Now;
    }
    
    private void ItemAdded()
    {
        AddedDateTime = DateTimeOffset.Now;
    }

    public void UpdateQuantity(int quantity, Inventory inventory)
    {
        Quantity = quantity;
        TotalPrice = quantity * inventory.Price;
        ItemUpdated();
    }
}