using System.Text;
using System.Text.Json;
using Confluent.Kafka;
using MediatR;
using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.ValueObjects;
using IPublisher = ShoppingCartService.Core.Interfaces.IPublisher;

namespace ShoppingCartService.Infrastructure.Publisher;

public class Publisher: IPublisher
{
    private readonly IProducer<string, ShoppingCart> _producer;

    public Publisher(AppSettings appSettings)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = appSettings.KafkaSettings.BootstrapServers,
            ClientId = Guid.NewGuid().ToString()
        }; 

        var producerBuilder = new ProducerBuilder<string, ShoppingCart>(config);
        producerBuilder.SetValueSerializer(new ShoppingCartSerializer());
        _producer = producerBuilder.Build();
    }

    public async Task<bool> ProcessMessage(string topic, string key, ShoppingCart value)
    {
        try
        {
            var message = new Message<string, ShoppingCart>
            {
                Key = key,
                Value = value
            };

            var deliveryReport = await _producer.ProduceAsync(topic, message);
            
            return deliveryReport.Status == PersistenceStatus.Persisted;
        }
        catch (Exception ex)
        {
            Console.WriteLine("---> Could not publish Checkout message due to: " + ex.Message);
            return false;
        }
    }
}
public class ShoppingCartSerializer : ISerializer<ShoppingCart>
{
    public byte[] Serialize(ShoppingCart data, SerializationContext context)
    {
        var jsonString = JsonSerializer.Serialize(data);
        return Encoding.UTF8.GetBytes(jsonString);
    }
}
