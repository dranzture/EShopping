namespace InvShopRevOrchestrator.Core.Dtos;

public class DeleteShoppingItemRequestDto
{
    public Guid ShoppingCartId { get; set; }
    public InventoryDto Inventory { get; set; }
}