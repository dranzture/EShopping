using ShoppingCartService.Core.Models;

namespace ShoppingCartService.Core.Entities;

public class ShoppingItem : BaseEntity
{
    public ShoppingItem(Guid inventoryId,Guid shoppingCartId, string username)
    {
        InventoryId = inventoryId;
        ShoppingCartId = shoppingCartId;
        UpdateCreatedFields(username);
    } 
    
    public ShoppingItem(){}//Required for EF
    
    public Guid ShoppingCartId;
    
    public Guid InventoryId{ get; private set; }

    public int Quantity { get; private set; }

    public decimal TotalPrice { get; private set; }
    
    
    public void UpdateQuantity(int quantity, Inventory inventory, string username)
    {
        Quantity = quantity;
        TotalPrice = quantity * inventory.Price;
        UpdateModifiedFields(username);
    }
}