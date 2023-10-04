using MediatR;
using OrderService.Core.Commands;
using OrderService.Core.Dtos;
using OrderService.Core.Interfaces;
using OrderService.Core.Notifications;

namespace OrderService.Core.Handlers;

public class CreateOrderNotificationHandler : INotificationHandler<CreateOrderNotification>
{
    private readonly IOrderRepository _orderRepository;
    private readonly CreateOrderDto _dto;

    public CreateOrderNotificationHandler(IOrderRepository orderRepository, CreateOrderDto dto)
    {
        _orderRepository = orderRepository;
        _dto = dto;
    }

    public async Task Handle(CreateOrderNotification notification, CancellationToken cancellationToken)
    {
        var createOrderCommand = new CreateOrderCommand(_orderRepository, _dto);
        if (!await createOrderCommand.CanExecute())
        {
            throw new ArgumentException("Order cannot be created");
        }

        await createOrderCommand.Execute();
    }
}

