using ShippingService.Core.Enums;

namespace ShippingService.Core.Entities;

public class ShippingItem : BaseEntity
{
    
    public ShippingItem(){}//For Ef
    public ShippingItem(Guid orderId, ShippingStatus status)
    {
        Id = new Guid();
        Status = status;
        OrderId = orderId;
    }
    
    public Guid OrderId { get; private set; }
    
    public ShippingStatus Status { get; private set; }
}