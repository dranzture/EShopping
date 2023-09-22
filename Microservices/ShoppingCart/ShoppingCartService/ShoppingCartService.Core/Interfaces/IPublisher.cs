namespace ShoppingCartService.Core.Interfaces;

public interface IPublisher<in TKey, in TValue>
{
    public const string CheckoutTopic = "checkout";
    public const string InventoryTopic = "inventory";
    
    Task<bool> ProcessMessage(string topic, TKey key, TValue value);
}