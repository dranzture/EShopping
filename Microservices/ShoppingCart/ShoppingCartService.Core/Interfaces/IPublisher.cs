namespace ShoppingCartService.Core.Interfaces;

public interface IPublisher<in T>
{
    public const string CheckoutTopic = "checkout_topic";
    public const string IncreaseInventoryTopic = "increase_inventory_quantity_topic";
    public const string DecreaseInventoryTopic = "decrease_inventory_quantity_topic";
    
    Task<bool> ProcessMessage(string topic, string key, T value);
}