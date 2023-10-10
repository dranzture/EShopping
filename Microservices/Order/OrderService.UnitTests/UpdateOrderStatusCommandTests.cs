using FluentAssertions;
using NSubstitute;
using OrderService.Core.Commands;
using OrderService.Core.Dtos;
using OrderService.Core.Entities;
using OrderService.Core.Enums;
using OrderService.Core.Interfaces;

namespace OrderService.UnitTests
{
    public class UpdateOrderStatusCommandTests
    {
        [Fact]
        public async Task CanExecute_Should_ReturnTrue_IfOrderExists()
        {
            // Arrange
            var shoppingCartId = Guid.NewGuid();
            var orderDto = new OrderDto
            {
                ShoppingCartId = shoppingCartId,
                Status = OrderStatus.Created
            };

            // Create a mock for IOrderRepository
            var orderRepository = Substitute.For<IOrderRepository>();
            orderRepository.GetByShoppingCartId(shoppingCartId).Returns(new Order());

            // Create an instance of the UpdateOrderStatusCommand
            var command = new UpdateOrderStatusCommand(orderRepository, orderDto);

            // Act
            var canExecute = await command.CanExecute();

            // Assert
            canExecute.Should().BeTrue(); // Use your preferred assertion library here
        }

        [Fact]
        public async Task CanExecute_Should_ReturnFalse_IfOrderDoesNotExist()
        {
            // Arrange
            var shoppingCartId = Guid.NewGuid();
            var orderDto = new OrderDto
            {
                ShoppingCartId = shoppingCartId,
                Status = OrderStatus.Created
            };

            // Create a mock for IOrderRepository
            var orderRepository = Substitute.For<IOrderRepository>();
            orderRepository.GetByShoppingCartId(shoppingCartId).Returns((Order)null); // Return null for non-existent order

            // Create an instance of the UpdateOrderStatusCommand
            var command = new UpdateOrderStatusCommand(orderRepository, orderDto);

            // Act
            var canExecute = await command.CanExecute();

            // Assert
            canExecute.Should().BeFalse(); // Use your preferred assertion library here
        }

        [Fact]
        public async Task Execute_Should_UpdateOrderStatus()
        {
            // Arrange
            var shoppingCartId = Guid.NewGuid();
            var orderDto = new OrderDto
            {
                ShoppingCartId = shoppingCartId,
                Status = OrderStatus.Created
            };

            // Create a mock for IOrderRepository
            var orderRepository = Substitute.For<IOrderRepository>();
            var existingOrder = new Order();
            orderRepository.GetByShoppingCartId(shoppingCartId).Returns(existingOrder);

            // Create an instance of the UpdateOrderStatusCommand
            var command = new UpdateOrderStatusCommand(orderRepository, orderDto);

            // Act
            await command.Execute();

            // Assert
            existingOrder.Status.Should().Be(orderDto.Status); // Use your preferred assertion library here
        }
    }
}
