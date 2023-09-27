namespace InvShopRevOrchestrator.Core.Dtos;

public class UpdateShoppingCartItemCommandDto
{
    public Guid ShoppingCartId { get; set; }
    public InventoryDto Inventory { get; set; }
    public int Quantity { get; set; }
    public string Username { get; set; }
}