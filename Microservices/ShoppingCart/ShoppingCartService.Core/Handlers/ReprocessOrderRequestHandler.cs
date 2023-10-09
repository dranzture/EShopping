using AutoMapper;
using MediatR;
using ShoppingCartService.Core.Dtos;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.Requests;

namespace ShoppingCartService.Core.Handlers;

public class ReprocessOrderRequestHandler : IRequestHandler<ReprocessOrderRequest>
{
    private readonly IShoppingCartService _shoppingCartService;

    public ReprocessOrderRequestHandler(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    public async Task Handle(ReprocessOrderRequest request, CancellationToken cancellationToken)
    {
        await _shoppingCartService.CheckoutShoppingCart(request._dto.ShoppingCartId, cancellationToken);
    }
}