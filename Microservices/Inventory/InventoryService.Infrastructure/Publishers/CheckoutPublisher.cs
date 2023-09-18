using System.Text.Json;
using Confluent.Kafka;
using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;
using InventoryService.Core.ValueObjects;
using Microsoft.Extensions.Logging;

namespace InventoryService.Infrastructure.Publishers;

public class CheckoutPublisher : ICheckoutPublisher<ShoppingCart>
{
    private readonly AppSettings _appSettings;

    public CheckoutPublisher(AppSettings appSettings)
    {
        _appSettings = appSettings;
    }

    public async Task<bool> ProcessMessage(ShoppingCart message)
    {
        try
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _appSettings.KafkaSettings.BootstrapServers
            };
            using var producer = new ProducerBuilder<string, string>(config).Build();
            var result = await producer.ProduceAsync("checkout",
                new Message<string, string> { Key = "checkout", Value = JsonSerializer.Serialize(message) });
            return result.Status == PersistenceStatus.Persisted;
        }
        catch (Exception ex)
        {
            Console.WriteLine("---> Could not publish Checkout message due to: " + ex.Message);
            return false;
        }
    }
}