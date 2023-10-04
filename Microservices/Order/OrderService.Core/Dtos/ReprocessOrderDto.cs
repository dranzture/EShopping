namespace OrderService.Core.Dtos;

public class ReprocessOrderDto
{
    public Guid OrderId { get; set; }
    public Guid ShoppingCartId { get; set; }
}