using NSubstitute;
using ShoppingCartService.Core.Commands;
using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.Models;

namespace ShoppingCartService.UnitTests;

public class DeleteFromShoppingCartCommandTests
{
            [Fact]
        public async Task CanExecute_ReturnsTrue_WhenCartIsNotCheckedOutAndItemIsInCart()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var username = "testuser";
            var inventoryId = Guid.NewGuid();
            var cart = new ShoppingCart(username, cartId);
            var inventory = new Inventory("Product", "Description", 10, 5.0m, 3.0m, 1.5m, 25.99m, username, inventoryId);
            cart.AddItem(inventory, 1, username);
            var mockRepository = Substitute.For<IShoppingCartRepository>();
            mockRepository.GetShoppingCartById(cartId).Returns(Task.FromResult(cart));

            var command = new DeleteFromShoppingCartCommand(mockRepository, cart, inventory, username);

            // Act
            var canExecute = await command.CanExecute();

            // Assert
            Assert.True(canExecute);
        }

        [Fact]
        public async Task CanExecute_ReturnsFalse_WhenCartIsCheckedOut()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var username = "testuser";
            var inventoryId = Guid.NewGuid();
            var cart = new ShoppingCart(username, cartId);
            cart.UpdateCheckoutStatus(ShoppingCart.CheckoutStatus.Completed); // Checked out
            var inventory = new Inventory("Product", "Description", 10, 5.0m, 3.0m, 1.5m, 25.99m, username, inventoryId);
            var mockRepository = Substitute.For<IShoppingCartRepository>();
            mockRepository.GetShoppingCartById(cartId).Returns(Task.FromResult(cart));

            var command = new DeleteFromShoppingCartCommand(mockRepository, cart, inventory, username);

            // Act
            var canExecute = await command.CanExecute();

            // Assert
            Assert.False(canExecute);
        }

        [Fact]
        public async Task CanExecute_ReturnsFalse_WhenItemIsNotInCart()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var username = "testuser";
            var inventoryId = Guid.NewGuid();
            var cart = new ShoppingCart(username, cartId);
            var inventory = new Inventory("Product", "Description", 10, 5.0m, 3.0m, 1.5m, 25.99m, username, inventoryId);
            var mockRepository = Substitute.For<IShoppingCartRepository>();
            mockRepository.GetShoppingCartById(cartId).Returns(Task.FromResult(cart));

            var command = new DeleteFromShoppingCartCommand(mockRepository, cart, inventory, username);

            // Act
            var canExecute = await command.CanExecute();

            // Assert
            Assert.False(canExecute);
        }

        [Fact]
        public async Task Execute_RemovesItemFromCartAndSavesChanges()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var username = "testuser";
            var inventoryId = Guid.NewGuid();
            var cart = new ShoppingCart(username, cartId);
            var inventory = new Inventory("Product", "Description", 10, 5.0m, 3.0m, 1.5m, 25.99m, username, inventoryId);
            cart.AddItem(inventory, 1, username);
            var mockRepository = Substitute.For<IShoppingCartRepository>();
            mockRepository.GetShoppingCartById(cartId).Returns(Task.FromResult(cart));

            var command = new DeleteFromShoppingCartCommand(mockRepository, cart, inventory, username);

            // Act
            await command.Execute();

            // Assert
            await mockRepository.Received(1).UpdateAsync(cart);
            await mockRepository.Received(1).SaveChangesAsync();

            // Ensure that the item is removed from the cart
            var removedItem = cart.ShoppingItems.FirstOrDefault(item => item.Item.Id == inventory.Id);
            Assert.Null(removedItem);
        }
}