using OrderService.Core.Helpers;
using ShippingService.Core.Dto;
using ShippingService.Core.Enums;
using ShippingService.Core.Notifications;

namespace ShippingService.Core.Entities;

public class ShippingItem : BaseEntity
{
    public ShippingItem(){}//For Ef
    public ShippingItem(Guid orderId, ShippingStatus status, string username)
    {
        Username = username;
        Id = Guid.NewGuid();
        UpdateStatus(status);
        OrderId = orderId;
        CreatedBy = username;
        CreatedDateTime = DateTime.Now;
    }
    
    public Guid OrderId { get; private set; }
    
    public ShippingStatus Status { get; private set; }
    
    public string Username { get;private set;}
    
    private void AddDomainEvent(DomainEventBase domainEvent)
    {
        RegisterDomainEvent(domainEvent);
    }
    
    public void UpdateStatus(ShippingStatus status)
    {
        Status = status;
        AddDomainEvent(new UpdateShippingStatusNotification(new ShippingItemDto()
        {
            Id = Id,
            OrderId = OrderId,
            Status = status,
            Username = Username
        }));
    }
    
    public void ClearDomainEvents()
    {
        base.ClearDomainEvents();
    }

}