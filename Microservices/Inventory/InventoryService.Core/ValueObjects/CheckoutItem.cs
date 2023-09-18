namespace InventoryService.Core.ValueObjects;

public class CheckoutItem
{
    public Guid ShoppingCartId { get; set; }
    public string Username { get; set; }
    public decimal Total { get; set; }
}