using Confluent.Kafka;
using InventoryService.Core.ValueObjects;
using Microsoft.Extensions.Hosting;

namespace InventoryService.Infrastructure.Consumer;

public class ShoppingCartConsumerService : IHostedService
{
    private readonly AppSettings _settings;
    private readonly string _topic = "update_cart_status";
    private IConsumer<Ignore, string> _consumer;
    private CancellationTokenSource _cancellationTokenSource;

    public ShoppingCartConsumerService(AppSettings settings)
    {
        _settings = settings;
        var config = new ProducerConfig
        {
            BootstrapServers = _settings.KafkaSettings.BootstrapServers
        }; 
        _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        _consumer.Subscribe(_topic);

        _cancellationTokenSource = new CancellationTokenSource();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {

        Task.Run(() => StartConsumerLoop(_cancellationTokenSource.Token), cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _cancellationTokenSource.Cancel();
        _consumer.Close();
        _consumer.Dispose();
        return Task.CompletedTask;
    }

    private void StartConsumerLoop(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.Consume(cancellationToken);
                    Console.WriteLine($"Received message: {consumeResult.Message.Value}");
                    // Handle the received message here
                }
                catch (ConsumeException ex)
                {
                    Console.Error.WriteLine($"Error consuming from Kafka: {ex.Error.Reason}");
                }
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("---> Consumer stopped.");
        }
    }
}