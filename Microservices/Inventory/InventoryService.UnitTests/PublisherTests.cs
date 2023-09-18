using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;
using InventoryService.Core.ValueObjects;
using InventoryService.Infrastructure.Publishers;
using Microsoft.Extensions.Logging;

namespace InventoryService.Tests;

public class PublisherTests
{

    private readonly AppSettings _appSettings = new AppSettings()
    {
        KafkaSettings = new KafkaSettings()
        {
            BootstrapServers = "localhost:9092"
        }
    };

    [Fact]
    public async Task CheckoutPublisher_CanPublish_Should_Return_True()
    {
        var message = new ShoppingCart(1, "dranzture");
        message.AddItem(new ShoppingItem()
        {
            Amount = 1,
            InventoryId = new Guid("75fb07b8-3d6f-486d-be9a-3bb799222a83"),
            InventoryName = "TestInv",
            TotalPrice = 10,
            AddedDateTime = DateTimeOffset.Now
        }, "dranzture");
        
        var _publisher = new CheckoutPublisher(_appSettings);
        
        var result = await _publisher.ProcessMessage(message);
        Assert.True(result);
    }
}