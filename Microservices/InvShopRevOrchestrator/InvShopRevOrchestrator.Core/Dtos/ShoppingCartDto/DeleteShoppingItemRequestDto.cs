namespace InvShopRevOrchestrator.Core.Dtos;

public class DeleteShoppingItemRequestDto
{
    public ShoppingCartDto ShoppingCartDto { get; set; }
    public InventoryDto InventoryDto { get; set; }
}