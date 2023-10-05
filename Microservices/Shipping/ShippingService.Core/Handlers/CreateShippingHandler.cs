using MediatR;
using ShippingService.Core.Commands;
using ShippingService.Core.Interfaces;
using ShippingService.Core.Notifications;

namespace ShippingService.Core.Handlers;

public class CreateShippingHandler : INotificationHandler<CreateShippingNotification>
{
    private readonly IShippingItemRepository _repository;

    public CreateShippingHandler(IShippingItemRepository repository)
    {
        _repository = repository;
    }
    
    public async Task Handle(CreateShippingNotification notification, CancellationToken cancellationToken)
    {
        var createShippingCommand = new CreateShippingCommand(_repository, notification._dto);
        if (!await createShippingCommand.CanExecute())
        {
            throw new ArgumentException("Cannot create shipping");
        }
        await createShippingCommand.Execute();
    }
}