using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;
using InventoryService.Core.ValueObjects;
using InventoryService.Infrastructure.Publishers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace InventoryService.Tests;

public class PublisherProductionTests
{
    private readonly AppSettings _appSettings = new AppSettings()
    {
        KafkaSettings = new KafkaSettings()
        {
            BootstrapServers = "localhost:9092"
        }
    };





    [Fact]
    public async Task CheckoutPublisher_Publish_Should_Return_True()
    {
        var _publisher = Substitute.For<ICheckoutPublisher<ShoppingCart>>();
        _publisher.ProcessMessage(Arg.Any<ShoppingCart>()).Returns(Task.FromResult(true));
        var message = new ShoppingCart(1, "dranzture");
        message.AddItem(new ShoppingItem()
        {
            Amount = 1,
            InventoryId = new Guid("75fb07b8-3d6f-486d-be9a-3bb799222a83"),
            InventoryName = "TestInv",
            TotalPrice = 10,
            AddedDateTime = DateTimeOffset.Now
        }, "dranzture");


        _publisher.ProcessMessage(Arg.Any<ShoppingCart>()).Returns(Task.FromResult(true));


        var result = await _publisher.ProcessMessage(message);
        Assert.True(result);
    }
}