using MediatR;
using OrderService.Core.Dtos;
using OrderService.Core.Interfaces;
using OrderService.Core.Notifications;

namespace OrderService.Core.Handlers;

public class CreateShippingNotificationHandler : INotificationHandler<CreateShippingNotification>
{
    private readonly IMessagePublisher<ShippingDto> _publisher;

    public CreateShippingNotificationHandler(IMessagePublisher<ShippingDto> publisher)
    {
        _publisher = publisher;
    }
    
    public async Task Handle(CreateShippingNotification notification, CancellationToken cancellationToken)
    {
        await _publisher.ProcessMessage(IMessagePublisher<ShippingDto>.CreateShippingTopic, new Guid().ToString(),notification._dto);
    }
}