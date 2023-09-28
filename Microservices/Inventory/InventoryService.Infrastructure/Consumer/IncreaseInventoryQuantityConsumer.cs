using System.Text.Json;
using Confluent.Kafka;
using InventoryService.Core.Dtos;
using InventoryService.Core.Requests;
using InventoryService.Core.ValueObjects;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace InventoryService.Infrastructure.Consumer;

public class IncreaseInventoryQuantityConsumer : BackgroundService
{
    private readonly IMediator _mediator;
    private const string Topic = "increase_inventory_quantity_topic";
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly CancellationTokenSource _cancellationTokenSource;

    public IncreaseInventoryQuantityConsumer(AppSettings settings, IMediator mediator)
    {
        _mediator = mediator;
        var config = new ConsumerConfig
        {
            BootstrapServers = settings.KafkaSettings.BootstrapServers,
            GroupId = new Guid().ToString()
        }; 
        _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        _consumer.Subscribe(Topic);

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
                    var inventoryQuantityDto = JsonSerializer.Deserialize<ChangeInventoryQuantityDto>(consumeResult.Message.Value);
                    _mediator.Send(new IncreaseInventoryQuantityRequest(inventoryQuantityDto), cancellationToken);
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