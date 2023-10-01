using CheckoutService.Core.Dtos;
using CheckoutService.Core.Interfaces;
using CheckoutService.Core.ValueObjects;
using Confluent.Kafka;

namespace CheckoutService.Infrastructure.Publisher;

public class OrderPublisher : IMessageBusPublisher<OrderDto>
{
    private readonly IProducer<string, OrderDto> _producer;
    
    public OrderPublisher(AppSettings appSettings)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = appSettings.KafkaSettings!.BootstrapServers,
            ClientId = Guid.NewGuid().ToString(),
            Acks = Acks.Leader
        }; 

        var producerBuilder = new ProducerBuilder<string, OrderDto>(config);
        producerBuilder.SetValueSerializer(new OrderSerializer());
        _producer = producerBuilder.Build();
    }
    public async Task<bool> ProcessMessage(string topic, string key, OrderDto value)
    {
        try
        {
            var message = new Message<string, OrderDto>
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
    
    private class OrderSerializer : ISerializer<OrderDto>
    {
        public byte[] Serialize(OrderDto data, SerializationContext context)
        {
            return Helpers.SerializerHelper.Serializer(data);
        }
    }
}