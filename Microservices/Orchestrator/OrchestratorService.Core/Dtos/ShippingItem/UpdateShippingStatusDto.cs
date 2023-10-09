using OrchestratorService.Core.Enums;

namespace OrchestratorService.Core.Dtos.ShippingItem;

public class UpdateShippingStatusDto
{
    public Guid Id { get; set; }
    
    public ShippingStatus Status { get; set; }
}