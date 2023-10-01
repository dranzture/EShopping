using System;
using System.Threading;
using System.Threading.Tasks;
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
    private readonly IMapper _mapper;

    public ShoppingCartCheckoutRequestHandler(IPaymentMethodRepository repository,
        IMessageBusPublisher<ShoppingCartDto> shoppingCartPublisher,
        IMediator mediator,
        IMapper mapper)
    {
        _repository = repository;
        _shoppingCartPublisher = shoppingCartPublisher;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task Handle(ShoppingCartCheckoutRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var paymentMethod = await _repository.GetByUsername(request.ShoppingCart.Username, cancellationToken);

            var shoppingCart = _mapper.Map<ShoppingCart>(request.ShoppingCart);
            var processPaymentCommand = new ProcessPaymentCommand(paymentMethod.SelectedCreditCard, shoppingCart);

            shoppingCart.UpdateStatus(CheckoutStatus.Completed);

            await _shoppingCartPublisher.ProcessMessage(
                IMessageBusPublisher<ShoppingCartDto>.ProcessShoppingCartResponseTopic,
                new Guid().ToString(), _mapper.Map<ShoppingCartDto>(shoppingCart));

            if (await processPaymentCommand.CanExecute())
            {
                await processPaymentCommand.Execute();

                await _mediator.Send(new CreateOrderRequest(request.ShoppingCart, OrderStatus.Created),
                    cancellationToken);
            }
            else
            {
                await _mediator.Send(new CreateOrderRequest(request.ShoppingCart, OrderStatus.PaymentFailed),
                    cancellationToken);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("---> Could not publish Checkout message due to: " + ex.Message);
        }
    }
    
}