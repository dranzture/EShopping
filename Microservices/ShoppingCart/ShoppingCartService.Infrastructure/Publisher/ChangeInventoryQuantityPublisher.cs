using Confluent.Kafka;
using ShoppingCartService.Core.Dtos;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.ValueObjects;

namespace ShoppingCartService.Infrastructure.Publisher;

public class ChangeInventoryQuantityPublisher : IPublisher<ChangeInventoryQuantityDto>
{
    private readonly IProducer<string, ChangeInventoryQuantityDto> _producer;

    public ChangeInventoryQuantityPublisher(AppSettings appSettings)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = appSettings.KafkaSettings.BootstrapServers,
            ClientId = Guid.NewGuid().ToString(),
            Acks = Acks.Leader
        }; 

        var producerBuilder = new ProducerBuilder<string, ChangeInventoryQuantityDto>(config);
        producerBuilder.SetValueSerializer(new ChangeInventoryQuantitySerializer());
        _producer = producerBuilder.Build();
    }
    public async Task<bool> ProcessMessage(string topic, string key, ChangeInventoryQuantityDto value)
    {
        
        var message = new Message<string, ChangeInventoryQuantityDto>
        {
            Key = key,
            Value = value
        };

        var deliveryReport = await _producer.ProduceAsync(topic, message);
        Console.WriteLine($"---> Produced message on topic of change inventory quantity on topic:{topic}");
        return deliveryReport.Status == PersistenceStatus.Persisted;
    }
    private class ChangeInventoryQuantitySerializer : ISerializer<ChangeInventoryQuantityDto>
    {
        public byte[] Serialize(ChangeInventoryQuantityDto data, SerializationContext context)
        {
            return Helpers.PublisherHelpers.PublishSerializationHelper(data);
        }
    }
}