using MediatR;
using OrderService.Core.Commands;
using OrderService.Core.Dtos;
using OrderService.Core.Interfaces;
using OrderService.Core.Notifications;

namespace OrderService.Core.Handlers;

public class UpdateOrderStatusNotificationHandler : INotificationHandler<UpdateOrderStatusNotification>
{
    private readonly IOrderRepository _orderRepository;
    private readonly UpdateOrderStatusDto _updateOrderStatusDto;

    public UpdateOrderStatusNotificationHandler(IOrderRepository orderRepository, UpdateOrderStatusDto updateOrderStatusDto)
    {
        _orderRepository = orderRepository;
        _updateOrderStatusDto = updateOrderStatusDto;
    }
    public async Task Handle(UpdateOrderStatusNotification statusNotification, CancellationToken cancellationToken)
    {
        var updateOrderStatusCommand = new UpdateOrderStatusCommand(_orderRepository, _updateOrderStatusDto);
        if (!await updateOrderStatusCommand.CanExecute())
        {
            throw new ArgumentException("Order cannot be updated");
        }

        await updateOrderStatusCommand.Execute();
    }
}