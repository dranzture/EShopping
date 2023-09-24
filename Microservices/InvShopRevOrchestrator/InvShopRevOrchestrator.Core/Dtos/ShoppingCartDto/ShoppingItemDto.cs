namespace InvShopRevOrchestrator.Core.Dtos;

public class ShoppingItemDto
{
    public Guid ShoppingCartId { get; set; }
    
    public Guid InventoryId { get; set;}
    
    public int Quantity { get; set;}
    
    public decimal Price { get; set;}
    
    public DateTimeOffset AddedDateTime { get; set;}
}