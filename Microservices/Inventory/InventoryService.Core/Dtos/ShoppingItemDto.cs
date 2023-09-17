namespace InventoryService.Core.Dtos;

public class ShoppingItemDto
{
    public Guid InventoryId { get; set;}
    
    public int Amount { get; set;}
    
    public decimal Price { get; set;}
    
    public DateTimeOffset AddedDateTime { get; set;}
}