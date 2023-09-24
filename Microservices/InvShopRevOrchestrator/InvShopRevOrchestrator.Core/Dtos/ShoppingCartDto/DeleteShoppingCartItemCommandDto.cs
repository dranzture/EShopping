namespace InvShopRevOrchestrator.Core.Dtos;

public class DeleteShoppingCartItemCommandDto
{
    public ShoppingCartDto ShoppingCart { get; set; }
    public InventoryDto Inventory { get; set; }
}