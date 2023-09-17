namespace InventoryService.Core.Dtos;

public class ShoppingCartDto
{
    public Guid? Id { get; set; }
    
    public int UserId { get; set; }
    
    List<ShoppingItemDto> ShoppingItems { get; set; }
}