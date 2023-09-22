using MediatR;
using ShoppingCartService.Core.Models;

namespace InventoryService.Core.Requests;

public class UpdateShoppingCartRequest : IRequest
{
    public readonly ShoppingCart _cart;

    public UpdateShoppingCartRequest(ShoppingCart cart)
    {
        _cart = cart;
    }
}