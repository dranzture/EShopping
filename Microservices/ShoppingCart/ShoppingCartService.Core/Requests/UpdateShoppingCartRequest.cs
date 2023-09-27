using MediatR;
using ShoppingCartService.Core.Entities;

namespace ShoppingCartService.Core.Requests;

public class UpdateShoppingCartRequest : IRequest
{
    public readonly ShoppingCart _cart;

    public UpdateShoppingCartRequest(ShoppingCart cart)
    {
        _cart = cart;
    }
}