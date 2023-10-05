using NSubstitute;
using ShoppingCartService.Core.Commands;
using ShoppingCartService.Core.Dtos;
using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.ValueObjects;

namespace ShoppingCartService.UnitTests;

public class CheckoutShoppingCartCommandTests
{
    [Fact]
    public async Task CanExecute_ReturnsTrue_WhenCartIsNotEmptyAndStatusIsNone()
    {
        // Arrange

        var repository = Substitute.For<IShoppingCartRepository>();
        var publisher1 = Substitute.For<IPublisher<ShoppingCartDto>>();

        var cart = new ShoppingCart("testuser");
        cart.AddItem(new Inventory(), 1, "testuser"); // Add a sample item to make the cart not empty

        repository.GetShoppingCartById(cart.Id).Returns(cart);

        var command = new CheckoutShoppingCartCommand(repository, cart.Id, publisher1);

        // Act
        var canExecute = await command.CanExecute();

        // Assert
        Assert.True(canExecute);
    }

    [Fact]
    public async Task CanExecute_ReturnsFalse_WhenCartIsEmpty()
    {
        // Arrange

        var repository = Substitute.For<IShoppingCartRepository>();
        var publisher = Substitute.For<IPublisher<ShoppingCartDto>>();

        var cart = new ShoppingCart("testuser");

        repository.GetShoppingCartById(cart.Id).Returns(cart);

        var command = new CheckoutShoppingCartCommand(repository, cart.Id, publisher);

        // Act
        var canExecute = await command.CanExecute();

        // Assert
        Assert.False(canExecute);
    }

    [Fact]
    public async Task CanExecute_ReturnsFalse_WhenCartStatusIsNotNone()
    {
        // Arrange
        var repository = Substitute.For<IShoppingCartRepository>();
        var publisher1 = Substitute.For<IPublisher<ShoppingCartDto>>();

        var cart = new ShoppingCart("testuser");
        cart.UpdateCheckoutStatus(CheckoutStatus.InProgress);
        cart.AddItem(new Inventory(), 1, "testuser");

        repository.GetShoppingCartById(cart.Id).Returns(cart);

        var command = new CheckoutShoppingCartCommand(repository, cart.Id, publisher1);

        // Act
        var canExecute = await command.CanExecute();

        // Assert
        Assert.False(canExecute);
    }

    [Fact]
    public async Task Execute_SetsCartStatusToInProgress_AndCallsPublisherAndRepository()
    {
        // Arrange
        var repository = Substitute.For<IShoppingCartRepository>();
        var publisher1 = Substitute.For<IPublisher<ShoppingCartDto>>();
        

        var cart = new ShoppingCart("testuser");
        cart.AddItem(new Inventory(), 1, "testuser");

        repository.GetShoppingCartById(cart.Id).Returns(cart);

        var command = new CheckoutShoppingCartCommand(repository, cart.Id, publisher1);

        // Act
        await command.Execute();

        // Assert
        await publisher1.Received(1).ProcessMessage(
            IPublisher<ShoppingCart>.CheckoutTopic,
            Arg.Any<string>(),
            Arg.Any<ShoppingCartDto>());

        Assert.Equal(CheckoutStatus.InProgress, cart.Status);

        await repository.Received(1).UpdateAsync(cart);
        await repository.Received(1).SaveChangesAsync();
    }
}