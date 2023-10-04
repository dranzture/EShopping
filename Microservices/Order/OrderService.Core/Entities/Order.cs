using System;
using OrderService.Core.Dtos;
using OrderService.Core.Enums;
using OrderService.Core.Helpers;
using OrderService.Core.Notifications;

namespace OrderService.Core.Entities;

public class Order : BaseEntity
{
    public Order(Guid shoppingCartId, OrderStatus orderStatus, string username)
    {
        Id = new Guid();
        CreatedDateTime = DateTimeOffset.Now;
        CreatedBy = Username = username;
        ShoppingCartId = shoppingCartId;
        UpdateOrderStatus(OrderStatus);
        ShippingId = Guid.NewGuid();
    }

    public Guid ShoppingCartId { get; }

    public OrderStatus OrderStatus { get; protected set; }
    
    public string Username { get; protected set; }
    
    public Guid ShippingId { get; set; }

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
        OrderStatus = orderStatus;
        if (orderStatus == OrderStatus.Created)
        {
            AddDomainEvent(new CreateShippingNotification(new OrderDto()
            {
                Id = Id,
                ShoppingCartId = ShoppingCartId,
                OrderStatus = OrderStatus,
            }));
        }
    }
}