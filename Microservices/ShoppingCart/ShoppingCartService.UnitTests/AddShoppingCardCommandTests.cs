using NSubstitute;
using ShoppingCartService.Core.Commands;
using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.Interfaces;

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
            var shoppingCart = new ShoppingCart(  "testuser");
            var addShoppingCartCommand = new AddShoppingCart(mockRepository, "testuser");

            // Act
            var canExecute = await addShoppingCartCommand.CanExecute();

            // Assert
            Assert.True(canExecute);
        }

        [Fact]
        public async Task CanExecute_ReturnsFalse_WhenShoppingCartExists()
        {
            // Arrange
            var existingShoppingCart = new ShoppingCart(  "testuser");
            var mockRepository = Substitute.For<IShoppingCartRepository>();
            mockRepository.GetShoppingCartByUsername(existingShoppingCart.Username).Returns(Task.FromResult(existingShoppingCart));

            var addShoppingCartCommand = new AddShoppingCart(mockRepository, "testuser");

            // Act
            var canExecute = await addShoppingCartCommand.CanExecute();

            // Assert
            Assert.False(canExecute);
        }

        [Fact]
        public async Task GetResult_ReturnsAddedShoppingCart()
        {
            // Arrange
            var shoppingCart = new ShoppingCart(  "testuser");
            var mockRepository = Substitute.For<IShoppingCartRepository>();
            mockRepository.GetShoppingCartByUsername(shoppingCart.Username).Returns(Task.FromResult((ShoppingCart?)null));
            mockRepository.AddAsync(Arg.Any<ShoppingCart>()).Returns(Task.CompletedTask);
            mockRepository.SaveChangesAsync().Returns(true);
            
            var addShoppingCartCommand = new AddShoppingCart(mockRepository, "testuser");

            // Act
            await addShoppingCartCommand.Execute();
            var result = addShoppingCartCommand.GetResult();

            // Assert
            Assert.NotNull(result);
        }
}