using FluentAssertions;
using NSubstitute;
using ShippingService.Core.Commands;
using ShippingService.Core.Dto;
using ShippingService.Core.Entities;
using ShippingService.Core.Enums;
using ShippingService.Core.Interfaces;

namespace ShippingService.UnitTests
{
    public class CreateShippingCommandTests
    {
        [Fact]
        public async Task Execute_Should_CreateShippingItem()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var username = "testuser";

            // Create a fake ShippingItemDto
            var dto = new ShippingItemDto { OrderId = orderId, Username = username };

            // Create a mock for IShippingItemRepository
            var repository = Substitute.For<IShippingItemRepository>();

            // Create an instance of the CreateShippingCommand
            var command = new CreateShippingCommand(repository, dto);

            // Act
            await command.Execute();

            // Assert
            await repository.Received(1).AddAsync(Arg.Is<ShippingItem>(item =>
                item.OrderId == orderId &&
                item.Status == ShippingStatus.LabelCreated &&
                item.Username == username));
            await repository.Received(1).SaveChangesAsync();
        }

        [Fact]
        public async Task CanExecute_Should_ReturnTrue_IfShippingItemDoesNotExist()
        {
            // Arrange
            var orderId = Guid.NewGuid();

            // Create a fake ShippingItemDto
            var dto = new ShippingItemDto { OrderId = orderId };

            // Create a mock for IShippingItemRepository
            var repository = Substitute.For<IShippingItemRepository>();
            repository.GetByOrderId(orderId).Returns(Task.FromResult<ShippingItem?>(null));

            // Create an instance of the CreateShippingCommand
            var command = new CreateShippingCommand(repository, dto);

            // Act
            var canExecute = await command.CanExecute();

            // Assert
            canExecute.Should().BeTrue();
        }

        [Fact]
        public async Task CanExecute_Should_ReturnFalse_IfShippingItemExists()
        {
            // Arrange
            var orderId = Guid.NewGuid();

            // Create a fake ShippingItemDto
            var dto = new ShippingItemDto { OrderId = orderId };

            // Create a mock for IShippingItemRepository
            var repository = Substitute.For<IShippingItemRepository>();
            repository.GetByOrderId(orderId).Returns(Task.FromResult(new ShippingItem()));

            // Create an instance of the CreateShippingCommand
            var command = new CreateShippingCommand(repository, dto);

            // Act
            var canExecute = await command.CanExecute();

            // Assert
            canExecute.Should().BeFalse();
        }
    }
}
