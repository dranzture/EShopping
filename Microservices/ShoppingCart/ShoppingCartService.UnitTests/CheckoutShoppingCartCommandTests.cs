using NSubstitute;
using ShoppingCartService.Core.Commands;
using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.Models;

namespace ShoppingCartService.UnitTests;

public class CheckoutShoppingCartCommandTests
{
    [Fact]
    public async Task CanExecute_ReturnsTrue_WhenCartIsNotEmptyAndStatusIsNone()
    {
        // Arrange
        var cartId = Guid.NewGuid();

        var repository = Substitute.For<IShoppingCartRepository>();
        var publisher = Substitute.For<IPublisher<string, ShoppingCart>>();

        var cart = new ShoppingCart("testuser", cartId);
        cart.AddItem(new Inventory(), 1, "testuser"); // Add a sample item to make the cart not empty

        repository.GetShoppingCartById(cartId).Returns(cart);

        var command = new CheckoutShoppingCart(repository, cart, publisher);

        // Act
        var canExecute = await command.CanExecute();

        // Assert
        Assert.True(canExecute);
    }

    [Fact]
    public async Task CanExecute_ReturnsFalse_WhenCartIsEmpty()
    {
        // Arrange
        var cartId = Guid.NewGuid();

        var repository = Substitute.For<IShoppingCartRepository>();
        var publisher = Substitute.For<IPublisher<string, ShoppingCart>>();

        var cart = new ShoppingCart("testuser", cartId);

        repository.GetShoppingCartById(cartId).Returns(cart);

        var command = new CheckoutShoppingCart(repository, cart, publisher);

        // Act
        var canExecute = await command.CanExecute();

        // Assert
        Assert.False(canExecute);
    }

    [Fact]
    public async Task CanExecute_ReturnsFalse_WhenCartStatusIsNotNone()
    {
        // Arrange
        var cartId = Guid.NewGuid();

        var repository = Substitute.For<IShoppingCartRepository>();
        var publisher = Substitute.For<IPublisher<string, ShoppingCart>>();

        var cart = new ShoppingCart("testuser", cartId);
        cart.UpdateCheckoutStatus(ShoppingCart.CheckoutStatus.InProgress);
        cart.AddItem(new Inventory(), 1, "testuser");

        repository.GetShoppingCartById(cartId).Returns(cart);

        var command = new CheckoutShoppingCart(repository, cart, publisher);

        // Act
        var canExecute = await command.CanExecute();

        // Assert
        Assert.False(canExecute);
    }

    [Fact]
    public async Task Execute_SetsCartStatusToInProgress_AndCallsPublisherAndRepository()
    {
        // Arrange
        var cartId = Guid.NewGuid();

        var repository = Substitute.For<IShoppingCartRepository>();
        var publisher = Substitute.For<IPublisher<string, ShoppingCart>>();

        var cart = new ShoppingCart("testuser", cartId);
        cart.AddItem(new Inventory(), 1, "testuser");

        repository.GetShoppingCartById(cartId).Returns(cart);

        var command = new CheckoutShoppingCart(repository, cart, publisher);

        // Act
        await command.Execute();

        // Assert
        await publisher.Received(1).ProcessMessage(
            IPublisher<string, ShoppingCart>.CheckoutTopic,
            Arg.Any<string>(),
            cart);

        Assert.Equal(ShoppingCart.CheckoutStatus.InProgress, cart.Status);

        await repository.Received(1).UpdateAsync(cart);
        await repository.Received(1).SaveChangesAsync();
    }
}