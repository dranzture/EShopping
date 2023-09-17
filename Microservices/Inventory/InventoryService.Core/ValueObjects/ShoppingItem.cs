namespace InventoryService.Core.ValueObjects;

public record ShoppingItem
{
    public Guid InventoryId { get; set;}
    public int Amount { get; set;}
    
    public decimal Price { get; set;}
    
    public DateTimeOffset AddedDateTime { get; set;}
    
    public DateTimeOffset UpdatedDateTime { get; set;}
}