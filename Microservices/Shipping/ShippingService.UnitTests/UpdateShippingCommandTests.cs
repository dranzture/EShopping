using FluentAssertions;
using NSubstitute;
using ShippingService.Core.Commands;
using ShippingService.Core.Dto;
using ShippingService.Core.Entities;
using ShippingService.Core.Enums;
using ShippingService.Core.Interfaces;

namespace ShippingService.UnitTests
{
    public class UpdateShippingStatusCommandTests
    {
        [Fact]
        public async Task Execute_Should_UpdateShippingStatus()
        {
            // Arrange
            var shippingItemId = Guid.NewGuid();
            var newStatus = ShippingStatus.Shipped;
            var username = "testuser";
            var orderId = Guid.NewGuid();

            // Create a fake ShippingItem
            var shippingItem = new ShippingItem(orderId, ShippingStatus.Received, username);

            // Create a fake ShippingItemDto
            var dto = new ShippingItemDto { Id = shippingItemId, Status = newStatus, Username = username, OrderId = orderId };

            // Create a mock for IShippingItemRepository
            var repository = Substitute.For<IShippingItemRepository>();
            repository.GetById(shippingItemId).Returns(Task.FromResult(shippingItem));

            // Create an instance of the UpdateShippingStatusCommand
            var command = new UpdateShippingStatusCommand(repository, dto);

            // Act
            await command.Execute();

            // Assert
            shippingItem.Status.Should().Be(newStatus);
            await repository.Received(1).UpdateAsync(shippingItem);
            await repository.Received(1).SaveChangesAsync();
        }

        [Fact]
        public async Task CanExecute_Should_ReturnTrue_IfShippingItemExists()
        {
            // Arrange
            var shippingItemId = Guid.NewGuid();
            var username = "testuser";
            var orderId = Guid.NewGuid();

            // Create a fake ShippingItem
            var shippingItem = new ShippingItem(orderId, ShippingStatus.Received, username);

            // Create a fake ShippingItemDto
            var dto = new ShippingItemDto { Id = shippingItemId, Username = username, OrderId = orderId };

            // Create a mock for IShippingItemRepository
            var repository = Substitute.For<IShippingItemRepository>();
            repository.GetById(shippingItemId).Returns(Task.FromResult(shippingItem));

            // Create an instance of the UpdateShippingStatusCommand
            var command = new UpdateShippingStatusCommand(repository, dto);

            // Act
            var canExecute = await command.CanExecute();

            // Assert
            canExecute.Should().BeTrue();
        }

        [Fact]
        public async Task CanExecute_Should_ReturnFalse_IfShippingItemDoesNotExist()
        {
            // Arrange
            var shippingItemId = Guid.NewGuid();
            var username = "testuser";

            // Create a fake ShippingItemDto
            var dto = new ShippingItemDto { Id = shippingItemId, Username = username };

            // Create a mock for IShippingItemRepository
            var repository = Substitute.For<IShippingItemRepository>();
            repository.GetById(shippingItemId).Returns(Task.FromResult<ShippingItem?>(null));

            // Create an instance of the UpdateShippingStatusCommand
            var command = new UpdateShippingStatusCommand(repository, dto);

            // Act
            var canExecute = await command.CanExecute();

            // Assert
            canExecute.Should().BeFalse();
        }
    }
}
