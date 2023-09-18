using InventoryService.Core.Dtos;

namespace InventoryService.Core.Interfaces;

public interface IShoppingCartService
{
    Task<string> AddShoppingCart(ShoppingCartDto dto, string username, CancellationToken token = default);

    Task UpdateShoppingCart(ShoppingCartDto dto, string username, CancellationToken token = default);

    Task DeleteShoppingCart(Guid id, string username, CancellationToken token = default);

    Task<ShoppingItemDto> AddShoppingItem(ShoppingItemDto dto, Guid id, string username,CancellationToken token = default);

    Task<ShoppingItemDto> UpdateShoppingItem(Guid id, int amount, string username, CancellationToken token = default);

    Task<ShoppingItemDto> DeleteShoppingItem(Guid id, string username, CancellationToken token = default);
    
    Task CheckoutShoppingCart(Guid id, string username, CancellationToken token = default);
}