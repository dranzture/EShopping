using ShoppingCartService.Core.Dtos;

namespace ShoppingCartService.Core.Interfaces;

public interface IShoppingCartService
{
    Task<string> AddShoppingCart(string username, CancellationToken token = default);
    
    Task AddShoppingItem(Guid shoppingCartId, InventoryDto inventoryDto, int quantity,
        string username, CancellationToken token = default);

    Task UpdateShoppingItem(Guid shoppingCartId, InventoryDto inventoryDto,
        int quantity, string username, CancellationToken token = default);

    Task DeleteShoppingItem(Guid shoppingCartId, InventoryDto inventoryDto, string username, CancellationToken token = default);
    
    Task CheckoutShoppingCart(Guid shoppingCartId, CancellationToken token = default);
    
    Task<ShoppingCartDto> GetOrderDetails(Guid shoppingCartId, CancellationToken token = default);
    
    Task<ShoppingCartDto> GetShoppingCartByUsername(string username, CancellationToken token = default);
}
