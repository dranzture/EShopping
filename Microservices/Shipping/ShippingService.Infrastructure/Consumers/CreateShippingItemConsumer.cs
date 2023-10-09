using System.Text.Json;
using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Hosting;
using ShippingService.Core.Dto;
using ShippingService.Core.Notifications;
using ShippingService.Core.ValueObjects;

namespace ShippingService.Infrastructure.Consumers;

public class CreateShippingItemConsumer : BackgroundService
{
    private readonly IMediator _mediator;
    private readonly string _topic = "create_shipping_topic";
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly CancellationTokenSource _cancellationTokenSource;

    public CreateShippingItemConsumer(AppSettings settings, IMediator mediator)
    {
        _mediator = mediator;
        var config = new ConsumerConfig
        {
            BootstrapServers = settings.KafkaSettings!.BootstrapServers,
            GroupId = new Guid().ToString()
        };
        _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        _consumer.Subscribe(_topic);

        _cancellationTokenSource = new CancellationTokenSource();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Run(() => StartConsumerLoop(_cancellationTokenSource.Token), stoppingToken);
    }

    private async Task StartConsumerLoop(CancellationToken cancellationToken)
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

                    var shippingItemDto = JsonSerializer.Deserialize<ShippingItemDto>(consumeResult.Message.Value);
                    await _mediator.Publish(new CreateShippingNotification(shippingItemDto),
                        cancellationToken);
                }
                catch (ConsumeException ex)
                {
                    await Console.Error.WriteLineAsync($"Error consuming from Kafka: {ex.Error.Reason}");
                }
                catch (Exception ex)
                {
                    await Console.Error.WriteLineAsync($"Error consuming from Kafka: {ex.Message}");
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