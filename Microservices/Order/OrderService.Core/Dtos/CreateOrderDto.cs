namespace OrderService.Core.Dtos;

public class CreateOrderDto
{
    public OrderDto Order { get; set; }
    public string Username { get; set; }
}