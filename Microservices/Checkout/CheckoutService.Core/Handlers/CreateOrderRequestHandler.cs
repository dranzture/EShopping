﻿using CheckoutService.Core.Dtos;
using CheckoutService.Core.Interfaces;
using CheckoutService.Core.Requests;
using CheckoutService.Core.ValueObjects;
using MediatR;

namespace CheckoutService.Core.Handlers;

public class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest>
{
    private readonly IMessageBusPublisher<OrderDto> _orderPublisher;

    public CreateOrderRequestHandler(IMessageBusPublisher<OrderDto> orderPublisher)
    {
        _orderPublisher = orderPublisher;
    }
    
    public async Task Handle(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        await _orderPublisher.ProcessMessage(IMessageBusPublisher<OrderDto>.OrderTopic,
            new Guid().ToString(), new OrderDto()
            {
                Id = new Guid(),
                ShoppingCartId = request.ShoppingCart.Id,
                Status = OrderStatus.PaymentFailed
            });
    }
}