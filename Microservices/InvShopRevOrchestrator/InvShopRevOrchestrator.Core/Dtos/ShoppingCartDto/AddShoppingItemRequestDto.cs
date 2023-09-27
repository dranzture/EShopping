namespace InvShopRevOrchestrator.Core.Dtos;

public class AddShoppingItemRequestDto
{
    public Guid ShoppingCartId { get; set; }
    public InventoryDto Inventory { get; set; }
    public int Quantity { get; set; }
}