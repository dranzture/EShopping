namespace InvShopRevOrchestrator.Core.Dtos;

public class ShoppingCartDto
{
    public Guid Id { get; set; }
    
    public string Username { get; set; }
    
    public List<ShoppingItemDto> ShoppingItems { get; set; }
}
