namespace CheckoutService.Core.Interfaces;

public interface IMessageBusPublisher<in T>
{
    public const string ProcessShoppingCartResponseTopic = "process_checkout_response_topic";
    public const string OrderTopic = "order_topic";
    
    Task<bool> ProcessMessage(string topic, string key, T value);
}