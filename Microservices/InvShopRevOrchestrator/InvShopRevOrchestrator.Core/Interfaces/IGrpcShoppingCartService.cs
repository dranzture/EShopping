using InvShopRevOrchestrator.Core.Dtos;

namespace InvShopRevOrchestrator.Core.Interfaces;

public interface IGrpcShoppingCartService
{
    Task<Guid> AddShoppingCart(ShoppingCartDto request, CancellationToken token = default);

    Task AddShoppingItem(AddShoppingCartItemCommandDto request, CancellationToken token = default);

    Task UpdateShoppingItem(UpdateShoppingCartItemCommandDto request, CancellationToken token = default);

    Task DeleteShoppingItem(DeleteShoppingCartItemCommandDto request, CancellationToken token = default);

    Task CheckoutShoppingCart(ShoppingCartDto request, CancellationToken token = default);

    Task<ShoppingCartDto> GetOrderDetails(Guid id, CancellationToken token = default);
}