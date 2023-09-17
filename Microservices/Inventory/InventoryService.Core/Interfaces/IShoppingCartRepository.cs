using InventoryService.Core.Models;

namespace InventoryService.Core.Interfaces;

public interface IShoppingCartRepository : IRepository<ShoppingCart>
{
    Task<ShoppingCart?> GetShoppingCartByUserId(int userId, CancellationToken token = default);
    
    Task<ShoppingCart> GetShoppingCartById(Guid id, CancellationToken token = default);
}