using OrderService.Core.Dtos;
using OrderService.Core.Entities;
using OrderService.Core.Interfaces;
using OrderService.Core.Commands;
using NSubstitute;
using FluentAssertions;
using OrderService.Core.Enums;

namespace OrderService.CreateOrderCommandTests
{
    public class CreateOrderCommandTests
    {
        [Fact]
        public async Task CanExecute_ReturnsTrue_WhenOrderDoesNotExist()
        {
            // Arrange
            var createOrderDto = new OrderDto
            {
                ShoppingCartId = Guid.NewGuid(),
                Status = OrderStatus.Created,
                Username = "testuser"
            };

            var mockRepository = Substitute.For<IOrderRepository>();
            mockRepository.Queryable().Returns(Task.FromResult(Enumerable.Empty<Order>().AsQueryable()));

            var createOrderCommand = new CreateOrderCommand(mockRepository, createOrderDto);

            // Act
            var canExecute = await createOrderCommand.CanExecute();

            // Assert
            canExecute.Should().BeTrue();
        }

        [Fact]
        public async Task CanExecute_ReturnsFalse_WhenOrderExists()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var createOrderDto = new OrderDto
            {
                ShoppingCartId = guid,
                Status = OrderStatus.Created,
                Username = "testuser"
            };

            var existingOrder = new Order(guid, OrderStatus.Created, "testuser");

            var mockRepository = Substitute.For<IOrderRepository>();
            

            var createOrderCommand = new CreateOrderCommand(mockRepository, createOrderDto);

            // Act
            var canExecute = await createOrderCommand.CanExecute();

            // Assert
            canExecute.Should().BeFalse();
        }

        [Fact]
        public async Task Execute_AddsNewOrderAndSavesChanges()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var createOrderDto = new OrderDto
            {
                ShoppingCartId = guid,
                Status = OrderStatus.Created,
                Username = "testuser"
            };

            var mockRepository = Substitute.For<IOrderRepository>();
            var createOrderCommand = new CreateOrderCommand(mockRepository, createOrderDto);

            // Act
            await createOrderCommand.Execute();

            // Assert
            await mockRepository.Received().AddAsync(Arg.Any<Order>());
            await mockRepository.Received().SaveChangesAsync();
        }
    }
}
