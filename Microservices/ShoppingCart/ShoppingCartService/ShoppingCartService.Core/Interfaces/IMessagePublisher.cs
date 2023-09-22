namespace ShoppingCartService.Core.Interfaces;

public interface IMessagePublisher<in T>
{
    
    Task<bool> ProcessMessage(T message);
}