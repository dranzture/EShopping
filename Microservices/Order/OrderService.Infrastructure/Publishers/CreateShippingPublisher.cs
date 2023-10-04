using Confluent.Kafka;
using OrderService.Core.Dtos;
using OrderService.Core.Interfaces;
using OrderService.Core.ValueObjects;
using OrderService.Infrastructure.Helpers;

namespace OrderService.Infrastructure.Publishers;

public class CreateShippingPublisher : IMessagePublisher<OrderDto>
{
    private readonly IProducer<string, OrderDto> _producer;

    public CreateShippingPublisher(AppSettings appSettings)
    {
        var config = new ProducerConfig()
        {
            BootstrapServers = appSettings.KafkaSettings.BootstrapServers,
            ClientId = Guid.NewGuid().ToString(),
            Acks = Acks.Leader
        }; 

        var producerBuilder = new ProducerBuilder<string, OrderDto>(config);
        producerBuilder.SetValueSerializer(new CreateShippingPublisherSerializer());
        _producer = producerBuilder.Build();
    }
    public async Task<bool> ProcessMessage(string topic, string key, OrderDto value)
    {
        
        var message = new Message<string, OrderDto>()
        {
            Key = key,
            Value = value
        };

        var deliveryReport = await _producer.ProduceAsync(topic, message);
        Console.WriteLine($"---> Produced message on topic of create shipping on topic:{topic}");
        return deliveryReport.Status == PersistenceStatus.Persisted;
    }
    private class CreateShippingPublisherSerializer : ISerializer<OrderDto>
    {
        public byte[] Serialize(OrderDto data, SerializationContext context)
        {
            return PublisherHelpers.PublishSerializationHelper(data);
        }
    }
}