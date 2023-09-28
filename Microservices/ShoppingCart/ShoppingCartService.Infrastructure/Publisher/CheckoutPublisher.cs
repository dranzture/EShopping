using Confluent.Kafka;
using ShoppingCartService.Core.Dtos;
using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.ValueObjects;

namespace ShoppingCartService.Infrastructure.Publisher;

public class CheckoutPublisher: IPublisher<ShoppingCartDto>
{
    private readonly IProducer<string, ShoppingCartDto> _producer;

    public CheckoutPublisher(AppSettings appSettings)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = appSettings.KafkaSettings.BootstrapServers,
            ClientId = Guid.NewGuid().ToString(),
            Acks = Acks.Leader
        }; 

        var producerBuilder = new ProducerBuilder<string, ShoppingCartDto>(config);
        producerBuilder.SetValueSerializer(new ShoppingCartSerializer());
        _producer = producerBuilder.Build();
    }

    public async Task<bool> ProcessMessage(string topic, string key, ShoppingCartDto value)
    {
        try
        {
            var message = new Message<string, ShoppingCartDto>
            {
                Key = key,
                Value = value
            };

            var deliveryReport = await _producer.ProduceAsync(topic, message);
            
            Console.WriteLine("---> Produced message on topic of checkout");
            
            return deliveryReport.Status == PersistenceStatus.Persisted;
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("---> Could not publish Checkout message due to: " + ex.Message);
            return false;
        }
    }
    private class ShoppingCartSerializer : ISerializer<ShoppingCartDto>
    {
        public byte[] Serialize(ShoppingCartDto data, SerializationContext context)
        {
            return Helpers.PublisherHelpers.PublishSerializationHelper(data);
        }
    }
}

