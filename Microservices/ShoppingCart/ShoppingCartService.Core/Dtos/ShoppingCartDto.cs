
using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.ValueObjects;

namespace ShoppingCartService.Core.Dtos;

public class ShoppingCartDto
{
    public Guid? Id { get; set; }
    
    public string Username { get; set; }

    public CheckoutStatus Status { get; set; }
    
    public List<ShoppingItemDto> ShoppingItems { get; set; }
}