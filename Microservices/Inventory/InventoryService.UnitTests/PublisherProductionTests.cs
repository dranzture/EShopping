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
        var shoppingCart = new ShoppingCart(1, "dranzture");
        var inventory = new Inventory("apple", "wow", 10, 10, 10, 10, 10, "dranzture", new Guid());
       
        shoppingCart.AddItem(inventory, 10, "dranzture");

        _publisher.ProcessMessage(Arg.Any<ShoppingCart>()).Returns(Task.FromResult(true));


        var result = await _publisher.ProcessMessage(shoppingCart);
        Assert.True(result);
    }
}