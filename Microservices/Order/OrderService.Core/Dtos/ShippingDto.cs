namespace OrderService.Core.Dtos;

public class ShippingDto
{
    public Guid OrderId { get; set; }
    
    public string Username { get; set; }
}