namespace InventoryService.Core.Dtos;

public class ChangeInventoryQuantityDto
{
    public Guid InventoryId { get; set; }
    
    public int Quantity { get; set; }
    
}