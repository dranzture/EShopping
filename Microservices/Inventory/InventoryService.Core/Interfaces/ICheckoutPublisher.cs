using InventoryService.Core.Models;

namespace InventoryService.Core.Interfaces;

public interface ICheckoutPublisher<in T> : IMessagePublisher<ShoppingCart>
{
    
}