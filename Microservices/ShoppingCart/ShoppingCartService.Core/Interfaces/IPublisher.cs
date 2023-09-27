using ShoppingCartService.Core.Entities;

namespace ShoppingCartService.Core.Interfaces;

public interface IPublisher
{
    public const string CheckoutTopic = "checkout";
    public const string InventoryTopic = "inventory";
    
    Task<bool> ProcessMessage(string topic, string key, ShoppingCart value);
}