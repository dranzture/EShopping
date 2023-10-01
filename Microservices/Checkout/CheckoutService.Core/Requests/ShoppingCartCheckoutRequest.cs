using CheckoutService.Core.Dtos;
using MediatR;

namespace CheckoutService.Core.Requests;

public class ShoppingCartCheckoutRequest : IRequest
{
    public ShoppingCartDto ShoppingCart { get; }

    public ShoppingCartCheckoutRequest(ShoppingCartDto shoppingCart)
    {
        ShoppingCart = shoppingCart;
    }
}