using System.Text.Json;
using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Hosting;
using OrderService.Core.Dtos;
using OrderService.Core.Interfaces;
using OrderService.Core.Notifications;
using OrderService.Core.ValueObjects;

namespace OrderService.Infrastructure.Consumer;

public class OrderConsumer : BackgroundService
{
    private readonly IMediator _mediator;
    private readonly IOrderRepository _orderRepository;
    private readonly string _topic = "order_topic";
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly CancellationTokenSource _cancellationTokenSource;

    public OrderConsumer(AppSettings settings, IMediator mediator, IOrderRepository orderRepository)
    {
        _mediator = mediator;
        _orderRepository = orderRepository;
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

                    var orderDto = JsonSerializer.Deserialize<OrderDto>(consumeResult.Message.Value);
                    if (orderDto == null) continue;
                    if (await _orderRepository.GetByShoppingCartId(orderDto.ShoppingCartId, cancellationToken) == null)
                    {
                        await _mediator.Publish(new CreateOrderNotification(orderDto), cancellationToken);
                    }
                    else
                    {
                        await _mediator.Publish(new UpdateOrderStatusNotification(orderDto), cancellationToken);
                    }
                }
                catch (ConsumeException ex)
                {
                    Console.Error.WriteLine($"Error consuming from Kafka: {ex.Error.Reason}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error consuming from Kafka: {ex.Message}");
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