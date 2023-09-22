
using ShoppingCartService.Core.Models;

namespace ShoppingCartService.Core.Dtos;

public class ShoppingCartDto
{
    public Guid? Id { get; set; }
    
    public string Username { get; set; }
    

    public ShoppingCart.CheckoutStatus Status { get; private set; } = ShoppingCart.CheckoutStatus.None;
    
    public List<ShoppingItemDto> ShoppingItems { get; set; }
}