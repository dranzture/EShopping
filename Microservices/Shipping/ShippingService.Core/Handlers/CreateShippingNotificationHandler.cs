using MediatR;
using ShippingService.Core.Notifications;

namespace ShippingService.Core.Handlers;

public class CreateShippingNotificationHandler : INotificationHandler<CreateShippingNotification>
{
    public Task Handle(CreateShippingNotification notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}