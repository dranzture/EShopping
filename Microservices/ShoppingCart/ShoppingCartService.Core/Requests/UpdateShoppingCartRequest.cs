using MediatR;
using ShoppingCartService.Core.Dtos;

namespace ShoppingCartService.Core.Requests;

public class UpdateShoppingCartRequest : IRequest
{
    public ShoppingCartDto _cart { get; }

    public UpdateShoppingCartRequest(ShoppingCartDto cart)
    {
        _cart = cart;
    }
}