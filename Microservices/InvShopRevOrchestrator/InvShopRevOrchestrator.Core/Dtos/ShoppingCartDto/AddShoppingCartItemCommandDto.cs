namespace InvShopRevOrchestrator.Core.Dtos;

public class AddShoppingCartItemCommandDto
{
    public ShoppingCartDto ShoppingCart { get; set; }
    public InventoryDto Inventory { get; set; }
    public int Quantity { get; set; }
}