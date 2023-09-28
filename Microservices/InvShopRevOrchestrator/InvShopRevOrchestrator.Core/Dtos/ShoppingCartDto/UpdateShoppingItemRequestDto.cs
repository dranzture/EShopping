namespace InvShopRevOrchestrator.Core.Dtos;

public class UpdateShoppingItemRequestDto
{
    public Guid ShoppingCartId { get; set; }
    public InventoryDto Inventory { get; set; }
    public int Quantity { get; set; }    
}