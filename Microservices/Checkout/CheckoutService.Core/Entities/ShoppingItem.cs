namespace CheckoutService.Core.Entities;

public class ShoppingItem : BaseEntity
{

    
    public ShoppingItem(){}//Required for EF
    
    public Guid ShoppingCartId;
    
    public Guid InventoryId{ get; private set; }

    public int Quantity { get; private set; }

    public decimal TotalPrice { get; private set; }
    
}