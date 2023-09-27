using InvShopRevOrchestrator.Core.Dtos;

namespace InvShopRevOrchestrator.Core.Interfaces;

public interface IGrpcShoppingCartService
{
    Task<Guid> AddShoppingCart(ShoppingCartDto request, CancellationToken token = default);

    Task AddShoppingItem(AddShoppingCartItemCommandDto request, CancellationToken token = default);

    Task UpdateShoppingItem(UpdateShoppingCartItemCommandDto request, CancellationToken token = default);

    Task DeleteShoppingItem(DeleteShoppingCartItemCommandDto request, CancellationToken token = default);

    Task CheckoutShoppingCart(Guid shoppingCartId, CancellationToken token = default);

    Task<ShoppingCartDto> GetOrderDetails(Guid shoppingCartId, CancellationToken token = default);
    
    Task<ShoppingCartDto> GetShoppingCartByUsername(string username, CancellationToken token = default);
}