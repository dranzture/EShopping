namespace InvShopRevOrchestrator.Core.Dtos;

public class AddShoppingItemRequestDto
{
    public ShoppingCartDto ShoppingCartDto { get; set; }
    public InventoryDto InventoryDto { get; set; }
    public int Quantity { get; set; }
}