using ShoppingCartService.Core.Dtos;

namespace ShoppingCartService.Core.Interfaces;

public interface IShoppingCartService
{
    Task<string> AddShoppingCart(ShoppingCartDto dto, string username, CancellationToken token = default);
    
    Task AddShoppingItem(ShoppingCartDto shoppingCartDto, InventoryDto inventoryDto, int quantity,
        string username, CancellationToken token = default);

    Task UpdateShoppingItem(ShoppingCartDto shoppingCartDto, InventoryDto inventoryDto,
        int quantity, string username, CancellationToken token = default);

    Task DeleteShoppingItem(ShoppingCartDto shoppingCartDto, InventoryDto inventoryDto, string username, CancellationToken token = default);
    
    Task CheckoutShoppingCart(ShoppingCartDto dto, string username, CancellationToken token = default);
    
    Task<ShoppingCartDto> GetOrderDetails(Guid cartId, CancellationToken token = default);
}
