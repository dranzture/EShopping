using System.ComponentModel.DataAnnotations;
using CheckoutService.Core.ValueObjects;

namespace CheckoutService.Core.Entities;

public class ShoppingCart : BaseEntity
{
    public ShoppingCart()
    {
    } //Ef Required

    [Required] 
    public string Username { get; private set; }

    private readonly List<ShoppingItem> _shoppingItems = new List<ShoppingItem>();

    public IReadOnlyCollection<ShoppingItem> ShoppingItems => _shoppingItems;

    public CheckoutStatus Status { get; private set; }

    public void UpdateStatus(CheckoutStatus status)
    {
        Status = status;
    }
}