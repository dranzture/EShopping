namespace InventoryService.Core.Interfaces;

public interface IMessagePublisher<in T>
{
    
    Task<bool> ProcessMessage(T message);
}