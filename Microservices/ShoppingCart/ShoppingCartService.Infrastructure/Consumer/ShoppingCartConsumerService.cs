using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Hosting;
using ShoppingCartService.Core.Dtos;
using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.Requests;
using ShoppingCartService.Core.ValueObjects;

namespace ShoppingCartService.Infrastructure.Consumer;

public class ShoppingCartConsumerService : BackgroundService
{
    private readonly IMediator _mediator;
    private readonly string _topic = "process_checkout_response_topic";
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly CancellationTokenSource _cancellationTokenSource;

    public ShoppingCartConsumerService(AppSettings settings, IMediator mediator)
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
                    var shoppingCart = JsonSerializer.Deserialize<ShoppingCartDto>(consumeResult.Message.Value);
                    _mediator.Send(new UpdateShoppingCartRequest(shoppingCart!), cancellationToken);
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