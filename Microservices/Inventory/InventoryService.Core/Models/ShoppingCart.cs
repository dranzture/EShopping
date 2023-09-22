using System.ComponentModel.DataAnnotations;
using InventoryService.Core.ValueObjects;

namespace InventoryService.Core.Models;

public class ShoppingCart : BaseEntity
{
    public ShoppingCart(string username, Guid? id = null)
    {
        Username = username;
        CreatedBy = username;
        CreatedDateTime = DateTimeOffset.Now;
        _ShoppingItems = new List<ShoppingItem>();
        if (id.HasValue)
        {
            Id = id.Value;
        }
    }

    [Required] public string Username { get; private set; }

    private List<ShoppingItem> _ShoppingItems { get; set; }

    public IReadOnlyCollection<ShoppingItem> ShoppingItems => _ShoppingItems;


    public CheckoutStatus Status { get; private set; } = CheckoutStatus.None;

    public void AddItem(Inventory inventory, int amount, string username)
    {
        var item = _ShoppingItems.FirstOrDefault(x => x.Item.Id == inventory.Id);
        if (item != null) throw new ArgumentException("This item is already in the shopping cart");
        _ShoppingItems.Add(new ShoppingItem()
        {
            Item = inventory,
            Amount = amount,
            TotalPrice = inventory.Price * amount,
            AddedDateTime = DateTimeOffset.Now
        });
    }

    public void UpdateAmountOfItem(Inventory inventory, int amount, string username)
    {
        var item = _ShoppingItems.FirstOrDefault(x => x.Item.Id == inventory.Id);
        if (item == null) return;
        item.Amount = amount;
        item.UpdatedDateTime = DateTimeOffset.Now;
        item.TotalPrice = amount * inventory.Price;
        UpdateModifiedFields(username);
    }

    public void RemoveItem(Inventory inventory, string username)
    {
        var item = _ShoppingItems.FirstOrDefault(x => x.Item.Id == inventory.Id);
        if (item == null) return;
        _ShoppingItems.Remove(item);
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

    public enum CheckoutStatus
    {
        None,
        InProgress,
        Completed,
        Failed
    }
}