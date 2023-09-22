using System.Text.Json;
using Confluent.Kafka;
using InventoryService.Core.Requests;
using MediatR;
using Microsoft.Extensions.Hosting;
using ShoppingCartService.Core.Models;
using ShoppingCartService.Core.ValueObjects;

namespace ShoppingCartService.Infrastructure.Consumer;

public class ShoppingCartConsumerService : BackgroundService
{
    private readonly AppSettings _settings;
    private readonly IMediator _mediator;
    private readonly string _topic = "update_cart_status";
    private IConsumer<Ignore, string> _consumer;
    private CancellationTokenSource _cancellationTokenSource;

    public ShoppingCartConsumerService(AppSettings settings, IMediator mediator)
    {
        _settings = settings;
        _mediator = mediator;
        var config = new ProducerConfig
        {
            BootstrapServers = _settings.KafkaSettings.BootstrapServers
        }; 
        _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        _consumer.Subscribe(_topic);

        _cancellationTokenSource = new CancellationTokenSource();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Run(() => StartConsumerLoop(_cancellationTokenSource.Token), stoppingToken);
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
                    var shoppingCart = JsonSerializer.Deserialize<ShoppingCart>(consumeResult.Message.Value);
                    _mediator.Send(new UpdateShoppingCartRequest(shoppingCart), cancellationToken);
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
            _cancellationTokenSource.Cancel();
            _consumer.Close();
            _consumer.Dispose();
        }
    }
}