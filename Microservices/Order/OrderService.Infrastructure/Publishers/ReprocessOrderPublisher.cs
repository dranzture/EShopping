using Confluent.Kafka;
using OrderService.Core.Dtos;
using OrderService.Core.Interfaces;
using OrderService.Core.ValueObjects;
using OrderService.Infrastructure.Helpers;

namespace OrderService.Infrastructure.Publishers;

public class ReprocessOrderPublisher : IMessagePublisher<ReprocessOrderDto>
{
    private readonly IProducer<string, ReprocessOrderDto> _producer;

    public ReprocessOrderPublisher(AppSettings appSettings)
    {
        var config = new ProducerConfig()
        {
            BootstrapServers = appSettings.KafkaSettings.BootstrapServers,
            ClientId = Guid.NewGuid().ToString(),
            Acks = Acks.Leader
        }; 

        var producerBuilder = new ProducerBuilder<string, ReprocessOrderDto>(config);
        producerBuilder.SetValueSerializer(new ReprocessOrderPublisherSerializer());
        _producer = producerBuilder.Build();
    }
    public async Task<bool> ProcessMessage(string topic, string key, ReprocessOrderDto value)
    {
        
        var message = new Message<string, ReprocessOrderDto>()
        {
            Key = key,
            Value = value
        };

        var deliveryReport = await _producer.ProduceAsync(topic, message);
        Console.WriteLine($"---> Produced message on topic of create shipping on topic:{topic}");
        return deliveryReport.Status == PersistenceStatus.Persisted;
    }
    private class ReprocessOrderPublisherSerializer : ISerializer<ReprocessOrderDto>
    {
        public byte[] Serialize(ReprocessOrderDto data, SerializationContext context)
        {
            return PublisherHelpers.PublishSerializationHelper(data);
        }
    }
}