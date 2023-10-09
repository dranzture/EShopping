using OrderService.Core.Dtos;
using OrderService.Core.Enums;
using OrderService.Core.Interfaces;

namespace OrderService.Core.Commands;

public class ReprocessOrderCommand : ICommand
{
    private readonly Guid _orderId;
    private readonly IMessagePublisher<ReprocessOrderDto> _publisher;
    private readonly IOrderRepository _orderRepository;

    public ReprocessOrderCommand(IOrderRepository orderRepository, Guid orderId, IMessagePublisher<ReprocessOrderDto> publisher)
    {
        _orderRepository = orderRepository;
        _orderId = orderId;
        _publisher = publisher;
    }

    public async Task<bool> CanExecute()
    {
        var result = await _orderRepository.GetById(_orderId);
        return result is { Status: OrderStatus.PaymentFailed };
    }

    public async Task Execute()
    {
        var result = await _orderRepository.GetById(_orderId);
        await _publisher.ProcessMessage(IMessagePublisher<ReprocessOrderDto>.ReprocessOrderTopic, Guid.NewGuid().ToString(),
            new ReprocessOrderDto()
            {
                OrderId = result.Id,
                ShoppingCartId = result.ShoppingCartId,
            });
    }
}