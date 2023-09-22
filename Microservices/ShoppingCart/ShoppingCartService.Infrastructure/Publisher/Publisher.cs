using Confluent.Kafka;
using MediatR;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.ValueObjects;

namespace ShoppingCartService.Infrastructure.Publisher;

public class Publisher<TKey, TValue> : IPublisher<TKey, TValue>
{
    private readonly IProducer<TKey, TValue> _producer;
    private readonly AppSettings _appSettings;
    private readonly IMediator _mediator;

    public Publisher(AppSettings appSettings, IMediator mediator)
    {
        _appSettings = appSettings;
        _mediator = mediator;
        var config = new ProducerConfig
        {
            BootstrapServers = _appSettings.KafkaSettings.BootstrapServers
        }; 

        _producer = new ProducerBuilder<TKey, TValue>(config).Build();
    }

    public async Task<bool> ProcessMessage(string topic, TKey key, TValue value)
    {
        try
        {
            var message = new Message<TKey, TValue>
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