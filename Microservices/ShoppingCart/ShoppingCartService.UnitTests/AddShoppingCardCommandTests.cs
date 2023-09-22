using NSubstitute;
using ShoppingCartService.Core.Commands;
using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.Models;

namespace ShoppingCartService.UnitTests;

public class AddShoppingCardCommandTests
{
    [Fact]
        public async Task CanExecute_ReturnsTrue_WhenShoppingCartDoesNotExist()
        {
            // Arrange
            var mockRepository = Substitute.For<IShoppingCartRepository>();
            mockRepository.GetShoppingCartByUsername(Arg.Any<string>()).Returns(Task.FromResult<ShoppingCart?>(null));

            var guid = Guid.NewGuid();
            var shoppingCart = new ShoppingCart(  "testuser", guid);
            var addShoppingCartCommand = new AddShoppingCart(mockRepository, shoppingCart);

            // Act
            var canExecute = await addShoppingCartCommand.CanExecute();

            // Assert
            Assert.True(canExecute);
        }

        [Fact]
        public async Task CanExecute_ReturnsFalse_WhenShoppingCartExists()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var existingShoppingCart = new ShoppingCart(  "testuser", guid);
            var mockRepository = Substitute.For<IShoppingCartRepository>();
            mockRepository.GetShoppingCartByUsername(existingShoppingCart.Username).Returns(Task.FromResult(existingShoppingCart));

            var addShoppingCartCommand = new AddShoppingCart(mockRepository, existingShoppingCart);

            // Act
            var canExecute = await addShoppingCartCommand.CanExecute();

            // Assert
            Assert.False(canExecute);
        }

        [Fact]
        public async Task Execute_AddsShoppingCartAndSavesChanges()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var shoppingCart = new ShoppingCart(  "testuser", guid);
            var mockRepository = Substitute.For<IShoppingCartRepository>();
            var addShoppingCartCommand = new AddShoppingCart(mockRepository, shoppingCart);

            // Act
            await addShoppingCartCommand.Execute();

            // Assert
            await mockRepository.Received(1).AddAsync(shoppingCart);
            await mockRepository.Received(1).SaveChangesAsync();
        }

        [Fact]
        public void GetResult_ReturnsAddedShoppingCart()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var shoppingCart = new ShoppingCart(  "testuser", guid);
            var mockRepository = Substitute.For<IShoppingCartRepository>();
            var addShoppingCartCommand = new AddShoppingCart(mockRepository, shoppingCart);

            // Act
            addShoppingCartCommand.Execute().GetAwaiter().GetResult();
            var result = addShoppingCartCommand.GetResult();

            // Assert
            Assert.Equal(shoppingCart, result);
        }
}