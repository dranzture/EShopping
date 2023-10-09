using CheckoutService.Core.Dtos;
using CheckoutService.Core.Interfaces;
using CheckoutService.Core.Requests;
using MediatR;

namespace CheckoutService.Core.Handlers;

public class OrderRequestHandler : IRequestHandler<OrderRequest>
{
    private readonly IMessageBusPublisher<OrderDto> _orderPublisher;

    public OrderRequestHandler(IMessageBusPublisher<OrderDto> orderPublisher)
    {
        _orderPublisher = orderPublisher;
    }

    public async Task Handle(OrderRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _orderPublisher.ProcessMessage(IMessageBusPublisher<OrderDto>.OrderTopic,
                new Guid().ToString(), new OrderDto()
                {
                    Id = new Guid(),
                    ShoppingCartId = request.ShoppingCart.Id,
                    Status = request.Status,
                    Username = request.ShoppingCart.Username
                });
        }
        catch (Exception ex)
        {
            Console.WriteLine("---> Could not publish Create order message due to: " + ex.Message);
        }
    }
}