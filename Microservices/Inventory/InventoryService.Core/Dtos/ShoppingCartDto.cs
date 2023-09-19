namespace InventoryService.Core.Dtos;

public class ShoppingCartDto
{
    public Guid? Id { get; set; }
    
    public string Username { get; set; }
    
    List<ShoppingItemDto> ShoppingItems { get; set; }
}