using InvShopRevOrchestrator.Core.Dtos;

namespace InvShopRevOrchestrator.Core.Interfaces;

public interface IShoppingCartService
{
    Task<Guid> AddShoppingCart(ShoppingCartDto dto, CancellationToken token = default);
    
    Task AddShoppingItem(Guid shoppingCartId, InventoryDto inventoryDto, int quantity, string username, CancellationToken token = default);

    Task UpdateShoppingItem(Guid shoppingCartId, InventoryDto inventoryDto,
        int quantity, string username,  CancellationToken token = default);

    Task DeleteShoppingItem(Guid shoppingCartId, InventoryDto inventoryDto, string username,  CancellationToken token = default);
    
    Task CheckoutShoppingCart(Guid shoppingCartId, CancellationToken token = default);
    
    Task<ShoppingCartDto> GetShoppingCartByUsername(string username, CancellationToken token = default);
    
    Task<ShoppingCartDto> GetOrderDetails(Guid cartId, CancellationToken token = default);
}