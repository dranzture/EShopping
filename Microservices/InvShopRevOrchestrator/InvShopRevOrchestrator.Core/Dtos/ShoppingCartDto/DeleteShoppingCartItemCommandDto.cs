namespace InvShopRevOrchestrator.Core.Dtos;

public class DeleteShoppingCartItemCommandDto
{
    public Guid ShoppingCartId { get; set; }
    public InventoryDto Inventory { get; set; }
    public string Username { get; set; }
}