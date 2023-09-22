using ShoppingCartService.Core.Models;

namespace ShoppingCartService.Core.ValueObjects;

public record ShoppingItem
{
    public ShoppingItem(Inventory item, int amount, decimal totalPrice, Guid shoppingCartId)
    {
        Item = item;
        Amount = amount;
        TotalPrice = totalPrice;
        _shoppingCartId = shoppingCartId;
        ItemAdded();
    } 
    
    
    private readonly Guid _shoppingCartId;

    public Guid ShoppingCartId => _shoppingCartId;

    public Inventory Item { get; private set; }

    public int Amount { get; private set; }

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

    public void UpdateAmount(int amount, Inventory inventory)
    {
        Amount = amount;
        TotalPrice = amount * inventory.Price;
        ItemUpdated();
    }
}