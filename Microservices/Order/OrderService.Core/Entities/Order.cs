using System;
using OrderService.Core.Dtos;
using OrderService.Core.Enums;
using OrderService.Core.Helpers;
using OrderService.Core.Notifications;

namespace OrderService.Core.Entities;

public class Order : BaseEntity
{
    public Order(){}// Required by EF
    
    public Order(Guid shoppingCartId, OrderStatus orderStatus, string username)
    {
        Id = Guid.NewGuid();
        CreatedDateTime = DateTimeOffset.Now;
        CreatedBy = Username = username;
        ShoppingCartId = shoppingCartId;
        UpdateOrderStatus(orderStatus);
        ShippingId = Guid.NewGuid();
    }

    public Guid ShoppingCartId { get; private set; }

    public OrderStatus Status { get; protected set; }
    
    public string Username { get; protected set; }
    
    public Guid ShippingId { get; private set; }

    private void AddDomainEvent(DomainEventBase domainEvent)
    {
        RegisterDomainEvent(domainEvent);
    }

    public void ClearDomainEvents()
    {
        base.ClearDomainEvents();
    }

    public void UpdateOrderStatus(OrderStatus orderStatus)
    {
        Status = orderStatus;
        if (orderStatus == OrderStatus.Created)
        {
            AddDomainEvent(new CreateShippingNotification(new ShippingDto()
            {
                OrderId = Id,
                Username = Username
            }));
        }
    }
}