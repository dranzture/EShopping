using CheckoutService.Core.Dtos;
using CheckoutService.Core.Entities;
using CheckoutService.Core.ValueObjects;
using MediatR;

namespace CheckoutService.Core.Requests;

public class OrderRequest : IRequest
{
    public ShoppingCartDto ShoppingCart { get; }
    public OrderStatus Status { get; }

    public OrderRequest(ShoppingCartDto shoppingCart, OrderStatus status)
    {
        ShoppingCart = shoppingCart;
        Status = status;
    }
}