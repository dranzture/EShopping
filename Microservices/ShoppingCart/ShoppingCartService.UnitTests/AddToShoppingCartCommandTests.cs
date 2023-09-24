using NSubstitute;
using ShoppingCartService.Core.Commands;
using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.ValueObjects;

namespace ShoppingCartService.UnitTests;

public class AddToShoppingCartCommandTests
{
    [Fact]
    public async Task CanExecute_ReturnsTrue_WhenCartIsNotCheckedOutAndItemNotInCart()
    {
        // Arrange
        var username = "testuser";
        var inventory = new Inventory("Product", "Description", 10, 5.0m, 3.0m, 1.5m, 25.99m, username);
        var cart = new ShoppingCart(username);
        var mockRepository = Substitute.For<IShoppingCartRepository>();
        mockRepository.GetShoppingCartByUsername(username).Returns(Task.FromResult(cart));

        var command = new AddToShoppingCartCommand(mockRepository, cart, inventory, 2, username);

        // Act
        var canExecute = await command.CanExecute();

        // Assert
        Assert.True(canExecute);
    }

    [Fact]
    public async Task CanExecute_ReturnsFalse_WhenCartIsCheckedOut()
    {
        // Arrange
        var username = "testuser";
        var inventory = new Inventory("Product", "Description", 10, 5.0m, 3.0m, 1.5m, 25.99m, username);
        var cart = new ShoppingCart(username);
        cart.UpdateCheckoutStatus(CheckoutStatus.Completed); // Checked out
        var mockRepository = Substitute.For<IShoppingCartRepository>();
        mockRepository.GetShoppingCartByUsername(username).Returns(Task.FromResult(cart));

        var command = new AddToShoppingCartCommand(mockRepository, cart, inventory, 2, username);

        // Act
        var canExecute = await command.CanExecute();

        // Assert
        Assert.False(canExecute);
    }

    [Fact]
    public async Task CanExecute_ReturnsFalse_WhenItemIsAlreadyInCart()
    {
        // Arrange
        var username = "testuser";
        var inventory = new Inventory("Product", "Description", 10, 5.0m, 3.0m, 1.5m, 25.99m, username);
        var cart = new ShoppingCart(username);
        cart.AddItem(inventory, 1, username); // Add the item to the cart
        var mockRepository = Substitute.For<IShoppingCartRepository>();
        mockRepository.GetShoppingCartByUsername(username).Returns(Task.FromResult(cart));

        var command = new AddToShoppingCartCommand(mockRepository, cart, inventory, 2, username);

        // Act
        var canExecute = await command.CanExecute();

        // Assert
        Assert.False(canExecute);
    }

    [Fact]
    public async Task Execute_AddsItemToCartAndSavesChanges()
    {
        // Arrange
        var username = "testuser";
        var inventory = new Inventory("Product", "Description", 10, 5.0m, 3.0m, 1.5m, 25.99m, username);
        var cart = new ShoppingCart(username);
        var mockRepository = Substitute.For<IShoppingCartRepository>();
        mockRepository.GetShoppingCartById(cart.Id).Returns(Task.FromResult(cart));

        var command = new AddToShoppingCartCommand(mockRepository, cart, inventory, 2, username);

        // Act
        await command.Execute();

        // Assert
        mockRepository.Received(1).UpdateAsync(cart);
        mockRepository.Received(1).SaveChangesAsync();

        // Ensure that the item is added to the cart
        var addedItem = cart.ShoppingItems.FirstOrDefault(item => item.Item.Id == inventory.Id);
        Assert.NotNull(addedItem);
        Assert.Equal(2, addedItem.Quantity);
    }
}