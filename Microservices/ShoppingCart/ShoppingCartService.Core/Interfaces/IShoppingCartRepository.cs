using ShoppingCartService.Core.Entities;

namespace ShoppingCartService.Core.Interfaces;

public interface IShoppingCartRepository : IRepository<ShoppingCart>
{
    Task<ShoppingCart?> GetShoppingCartByUsername(string username, CancellationToken token = default);
    
    Task<ShoppingCart?> GetShoppingCartById(Guid id, CancellationToken token = default);
}