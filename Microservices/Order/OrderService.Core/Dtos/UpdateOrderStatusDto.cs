using OrderService.Core.Enums;

namespace OrderService.Core.Dtos;

public class UpdateOrderStatusDto
{
    public Guid Id { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public string Username { get; set; }
}