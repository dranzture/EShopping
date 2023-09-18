namespace InventoryService.Core.ValueObjects;

public record ShoppingItem
{
    public Guid InventoryId { get; set;}
    
    public string InventoryName { get; set;}
    public int Amount { get; set;}
    
    public decimal TotalPrice { get; set;}
    
    public DateTimeOffset AddedDateTime { get; set;}
    public DateTimeOffset UpdatedDateTime { get; set;}
}