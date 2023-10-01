using CheckoutService.Core.ValueObjects;

namespace CheckoutService.Core.Dtos;

public class ShoppingCartDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }

    public CheckoutStatus Status { get; private set; } = CheckoutStatus.None;

    public List<ShoppingItemDto> ShoppingItems { get; set; }
}