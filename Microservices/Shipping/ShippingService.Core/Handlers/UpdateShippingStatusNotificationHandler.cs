using MediatR;
using ShippingService.Core.Enums;
using ShippingService.Core.Interfaces;
using ShippingService.Core.Notifications;

namespace ShippingService.Core.Handlers;

public class UpdateShippingStatusNotificationHandler : INotificationHandler<UpdateShippingStatusNotification>
{
    private readonly IEmailSender _mailSender;

    public UpdateShippingStatusNotificationHandler(IEmailSender mailSender)
    {
        _mailSender = mailSender;
    }
    
    public async Task Handle(UpdateShippingStatusNotification statusNotification, CancellationToken cancellationToken)
    {
        var body = statusNotification._dto.Status switch
        {
            ShippingStatus.Delivered => "Shipping Delivered",
            ShippingStatus.Received => "Shipping Received",
            ShippingStatus.Shipped => "Shipping Shipped",
            ShippingStatus.InTransit => "Shipping In Transit",
            ShippingStatus.LabelCreated => "Shipping Label Created",
            _ => throw new ArgumentOutOfRangeException("Invalid Shipping Status")
        };

        await _mailSender.SendEmailAsync(statusNotification._dto.Username, 
            "eshopping@example.com", 
            "Update Shipping Status", 
            body);
    }
}