namespace InvShopRevOrchestrator.Core.Dtos;

public class ShoppingItemDto
{
    public Guid ShoppingCartId { get; set; }
    
    public Guid InventoryId { get; set;}
    
    public int Quantity { get; set;}
    
    public decimal TotalPrice { get; set;}
}