using OrderService.Core.Enums;

namespace OrderService.Core.Dtos;

public class OrderDto
{
    public Guid Id { get; set; }
    public Guid ShoppingCartId { get; set; }
    public OrderStatus Status { get; set; }
    public string Username { get; set; }
}