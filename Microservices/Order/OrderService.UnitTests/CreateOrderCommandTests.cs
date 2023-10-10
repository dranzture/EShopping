using FluentAssertions;
using NSubstitute;
using OrderService.Core.Commands;
using OrderService.Core.Dtos;
using OrderService.Core.Entities;
using OrderService.Core.Enums;
using OrderService.Core.Interfaces;

namespace OrderService.UnitTests
{
    public class CreateOrderCommandTests
    {
        [Fact]
        public async Task Execute_Should_CreateOrder()
        {
            // Arrange
            var shoppingCartId = Guid.NewGuid();
            var username = "testuser";
            var orderDto = new OrderDto { ShoppingCartId = shoppingCartId, Status = OrderStatus.Created, Username = username };

            // Create a mock for IOrderRepository
            var orderRepository = Substitute.For<IOrderRepository>();

            // Create an instance of the CreateOrderCommand
            var command = new CreateOrderCommand(orderRepository, orderDto);

            // Act
            await command.Execute();

            // Assert
            await orderRepository.Received(1).AddAsync(Arg.Is<Order>(order =>
                order.ShoppingCartId == shoppingCartId &&
                order.Status == OrderStatus.Created &&
                order.Username == username &&
                order.ShippingId != Guid.Empty));

            await orderRepository.Received(1).SaveChangesAsync();
        }

        [Fact]
        public async Task CanExecute_Should_ReturnTrue_IfOrderDoesNotExist()
        {
            // Arrange
            var shoppingCartId = Guid.NewGuid();
            var orderDto = new OrderDto { ShoppingCartId = shoppingCartId };

            // Create a mock for IOrderRepository
            var orderRepository = Substitute.For<IOrderRepository>();
            orderRepository.GetByShoppingCartId(shoppingCartId).Returns(Task.FromResult<Order>(null));

            // Create an instance of the CreateOrderCommand
            var command = new CreateOrderCommand(orderRepository, orderDto);

            // Act
            var canExecute = await command.CanExecute();

            // Assert
            canExecute.Should().BeTrue();
        }

        [Fact]
        public async Task CanExecute_Should_ReturnFalse_IfOrderExists()
        {
            // Arrange
            var shoppingCartId = Guid.NewGuid();
            var orderDto = new OrderDto { ShoppingCartId = shoppingCartId };

            // Create a mock for IOrderRepository
            var orderRepository = Substitute.For<IOrderRepository>();
            orderRepository.GetByShoppingCartId(shoppingCartId).Returns(Task.FromResult(new Order()));

            // Create an instance of the CreateOrderCommand
            var command = new CreateOrderCommand(orderRepository, orderDto);

            // Act
            var canExecute = await command.CanExecute();

            // Assert
            canExecute.Should().BeFalse();
        }
    }
}
