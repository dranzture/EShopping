namespace OrderService.Core.Interfaces;

public interface IMessagePublisher<in T>
{
    public const string ReprocessOrderTopic = "reprocess_order_topic";
    public const string CreateShippingTopic = "create_shipping_topic";
    
    Task<bool> ProcessMessage(string topic, string key, T value);
}