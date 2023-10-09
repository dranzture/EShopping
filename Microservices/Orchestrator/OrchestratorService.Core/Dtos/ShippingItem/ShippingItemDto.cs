using OrchestratorService.Core.Enums;

namespace OrchestratorService.Core.Dtos.ShippingItem;

public class ShippingItemDto
{
    public Guid Id { get; set; }
    
    public Guid OrderId { get; set; }

    public ShippingStatus Status { get; set; }
}