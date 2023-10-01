using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CheckoutService.Core.Dtos;
using CheckoutService.Core.Entities;
using CheckoutService.Core.Handlers;
using CheckoutService.Core.Requests;
using CheckoutService.Core.ValueObjects;
using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace CheckoutService.Infrastructure.Consumer;

public class CheckoutConsumerService : BackgroundService
{
    private readonly IMediator _mediator;
    private readonly string _topic = "checkout_topic";
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly CancellationTokenSource _cancellationTokenSource;

    public CheckoutConsumerService(AppSettings settings, IMediator mediator)
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
                    
                    var shoppingCartDto = JsonSerializer.Deserialize<ShoppingCartDto>(consumeResult.Message.Value);
                    _mediator.Send(new ShoppingCartCheckoutRequest(shoppingCartDto),
                        cancellationToken);
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