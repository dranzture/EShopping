using MediatR;
using OrderService.Core.Commands;
using OrderService.Core.Dtos;
using OrderService.Core.Interfaces;
using OrderService.Core.Notifications;

namespace OrderService.Core.Handlers;

public class CreateOrderNotificationHandler : INotificationHandler<CreateOrderNotification>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderNotificationHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(CreateOrderNotification notification, CancellationToken cancellationToken)
    {
        var createOrderCommand = new CreateOrderCommand(_orderRepository, notification.Dto);
        if (!await createOrderCommand.CanExecute())
        {
            throw new ArgumentException("Order cannot be created");
        }

        await createOrderCommand.Execute();
    }
}

