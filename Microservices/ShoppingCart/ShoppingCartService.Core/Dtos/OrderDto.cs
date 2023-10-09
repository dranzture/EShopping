namespace ShoppingCartService.Core.Dtos;

public class OrderDto
{
    public Guid Id { get; set; }
    public Guid ShoppingCartId { get; set; }
    public string Username { get; set; }
}