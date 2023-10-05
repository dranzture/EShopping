using System;
using System.Threading.Tasks;
using NSubstitute;
using ShoppingCartService.Core.Commands;
using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.ValueObjects;

namespace ShoppingCartService.UnitTests;

public class UpdateCheckoutStatusCommandTests
{
    [Fact]
    public async Task CanExecute_ReturnsTrue_WhenCartExistsAndStatusIsNotCompleted()
    {
        // Arrange
        var repository = Substitute.For<IShoppingCartRepository>();

        var existingCart = new ShoppingCart("testUser");

        repository.GetShoppingCartById(existingCart.Id).Returns(existingCart);

        var command = new UpdateCheckoutStatusCommand(repository, existingCart.Id, CheckoutStatus.Completed);

        // Act
        var canExecute = await command.CanExecute();

        // Assert
        Assert.True(canExecute);
    }

    [Fact]
    public async Task CanExecute_ReturnsFalse_WhenCartDoesNotExist()
    {
        // Arrange
        var repository = Substitute.For<IShoppingCartRepository>();
        repository.GetShoppingCartById(Arg.Any<Guid>()).Returns((ShoppingCart)null); // Cart does not exist

        var command = new UpdateCheckoutStatusCommand(repository, new Guid(), CheckoutStatus.Completed);

        // Act
        var canExecute = await command.CanExecute();

        // Assert
        Assert.False(canExecute);
    }

    [Fact]
    public async Task CanExecute_ReturnsFalse_WhenStatusIsCompleted()
    {
        // Arrange
        var repository = Substitute.For<IShoppingCartRepository>();

        var existingCart = new ShoppingCart("testUser");
        existingCart.UpdateCheckoutStatus(CheckoutStatus.Completed);
        repository.GetShoppingCartById(existingCart.Id).Returns(existingCart);

        var command = new UpdateCheckoutStatusCommand(repository, existingCart.Id, CheckoutStatus.InProgress);

        // Act
        var canExecute = await command.CanExecute();

        // Assert
        Assert.False(canExecute);
    }

    [Fact]
    public async Task Execute_UpdatesCartStatus_AndCallsRepository()
    {
        // Arrange
        var repository = Substitute.For<IShoppingCartRepository>();

        var existingCart = new ShoppingCart("testUser");

        repository.GetShoppingCartById(existingCart.Id).Returns(existingCart);

        var command = new UpdateCheckoutStatusCommand(repository, existingCart.Id, CheckoutStatus.Completed);

        // Act
        await command.Execute();

        // Assert
        Assert.Equal(CheckoutStatus.Completed, existingCart.Status);
        await repository.Received(1).SaveChangesAsync();
    }
}