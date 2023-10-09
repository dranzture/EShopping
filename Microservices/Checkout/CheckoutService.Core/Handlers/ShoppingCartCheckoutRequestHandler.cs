using AutoMapper;
using CheckoutService.Core.Commands;
using CheckoutService.Core.Dtos;
using CheckoutService.Core.Entities;
using CheckoutService.Core.Interfaces;
using CheckoutService.Core.Requests;
using CheckoutService.Core.ValueObjects;
using MediatR;

namespace CheckoutService.Core.Handlers;

public class ShoppingCartCheckoutRequestHandler : IRequestHandler<ShoppingCartCheckoutRequest>
{
    private readonly IPaymentMethodRepository _repository;
    private readonly IMessageBusPublisher<ShoppingCartDto> _shoppingCartPublisher;
    private readonly IMediator _mediator;

    public ShoppingCartCheckoutRequestHandler(IPaymentMethodRepository repository,
        IMessageBusPublisher<ShoppingCartDto> shoppingCartPublisher,
        IMediator mediator,
        IMapper mapper)
    {
        _repository = repository;
        _shoppingCartPublisher = shoppingCartPublisher;
        _mediator = mediator;
    }

    public async Task Handle(ShoppingCartCheckoutRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var paymentMethod =
                await _repository.GetDefaultCreditCardByUsername(request.ShoppingCart.Username, cancellationToken);

            var processPaymentCommand = new ProcessPaymentCommand(paymentMethod, request.ShoppingCart);

            request.ShoppingCart.Status = CheckoutStatus.Completed;

            await _shoppingCartPublisher.ProcessMessage(
                IMessageBusPublisher<ShoppingCartDto>.ProcessShoppingCartResponseTopic,
                new Guid().ToString(), request.ShoppingCart);

            if (await processPaymentCommand.CanExecute())
            {
                await processPaymentCommand.Execute();
            }
            //For testing purposes on Orders.

            var random = new Random().Next(0, 10);
            if (random % 2 == 0)
            {
                await _mediator.Send(new OrderRequest(request.ShoppingCart, OrderStatus.PaymentFailed),
                    cancellationToken);
            }
            else
            {
                await _mediator.Send(new OrderRequest(request.ShoppingCart, OrderStatus.Created),
                    cancellationToken);
            }
            
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("---> Could not publish Checkout message due to: " + ex.Message);
        }
    }
}