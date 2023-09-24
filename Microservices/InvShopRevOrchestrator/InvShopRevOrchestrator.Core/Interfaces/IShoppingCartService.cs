using InvShopRevOrchestrator.Core.Dtos;

namespace InvShopRevOrchestrator.Core.Interfaces;

public interface IShoppingCartService
{
    Task<Guid> AddShoppingCart(ShoppingCartDto dto, CancellationToken token = default);
    
    Task AddShoppingItem(ShoppingCartDto shoppingCartDto, InventoryDto inventoryDto, int quantity, CancellationToken token = default);

    Task UpdateShoppingItem(ShoppingCartDto shoppingCartDto, InventoryDto inventoryDto,
        int quantity, CancellationToken token = default);

    Task DeleteShoppingItem(ShoppingCartDto shoppingCartDto, InventoryDto inventoryDto, CancellationToken token = default);
    
    Task CheckoutShoppingCart(ShoppingCartDto dto, CancellationToken token = default);
    
    Task<ShoppingCartDto> GetOrderDetails(Guid cartId, CancellationToken token = default);
}