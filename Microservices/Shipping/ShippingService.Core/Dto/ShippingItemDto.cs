using ShippingService.Core.Enums;

namespace ShippingService.Core.Dto;

public class ShippingItemDto
{
    public Guid Id { get; set; }
    
    public Guid OrderId { get; set; }

    public ShippingStatus Status { get; set; }
    
    public string Username { get; set; }
}