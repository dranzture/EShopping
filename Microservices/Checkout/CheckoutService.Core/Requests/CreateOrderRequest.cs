using CheckoutService.Core.Dtos;
using CheckoutService.Core.Entities;
using CheckoutService.Core.ValueObjects;
using MediatR;

namespace CheckoutService.Core.Requests;

public class CreateOrderRequest : IRequest
{
    public ShoppingCartDto ShoppingCart { get; }
    public OrderStatus Status { get; }

    public CreateOrderRequest(ShoppingCartDto shoppingCart, OrderStatus status)
    {
        ShoppingCart = shoppingCart;
        Status = status;
    }
}