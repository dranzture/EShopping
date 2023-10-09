using MediatR;
using OrderService.Core.Commands;
using OrderService.Core.Dtos;
using OrderService.Core.Interfaces;
using OrderService.Core.Notifications;

namespace OrderService.Core.Handlers;

public class UpdateOrderStatusNotificationHandler : INotificationHandler<UpdateOrderStatusNotification>
{
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderStatusNotificationHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task Handle(UpdateOrderStatusNotification statusNotification, CancellationToken cancellationToken)
    {
        var updateOrderStatusCommand = new UpdateOrderStatusCommand(_orderRepository, statusNotification.Dto);
        if (!await updateOrderStatusCommand.CanExecute())
        {
            throw new ArgumentException("Order cannot be updated");
        }

        await updateOrderStatusCommand.Execute();
    }
}