using FluentAssertions;
using NSubstitute;
using OrderService.Core.Commands;
using OrderService.Core.Dtos;
using OrderService.Core.Entities;
using OrderService.Core.Enums;
using OrderService.Core.Interfaces;

namespace OrderService.UnitTests
{
    public class ReprocessOrderCommandTests
    {
        [Fact]
        public async Task Execute_Should_SendReprocessOrderMessage()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var orderStatus = OrderStatus.PaymentFailed;

            // Create a mock for IMessagePublisher<ReprocessOrderDto> with ProcessMessage method mocked
            var publisher = Substitute.For<IMessagePublisher<ReprocessOrderDto>>();
            publisher
                .ProcessMessage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<ReprocessOrderDto>())
                .Returns(Task.FromResult(true)); // You can use Task.FromResult for the return value

            // Create a mock for IOrderRepository
            var orderRepository = Substitute.For<IOrderRepository>();
            
            // Set up the mock to return an Order instance with desired properties when GetById is called
            orderRepository.GetById(orderId).Returns(new Order(orderId, orderStatus, "testuser"));

            // Create an instance of the ReprocessOrderCommand with the mocked repository
            var command = new ReprocessOrderCommand(orderRepository, orderId, publisher);

            // Act
            await command.Execute();

            // Assert
            await publisher.Received(1).ProcessMessage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<ReprocessOrderDto>());
        }

        [Fact]
        public async Task CanExecute_Should_ReturnTrue_IfOrderStatusIsPaymentFailed()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var orderStatus = OrderStatus.PaymentFailed;

            // Create a mock for IMessagePublisher<ReprocessOrderDto>
            var publisher = Substitute.For<IMessagePublisher<ReprocessOrderDto>>();

            // Create a mock for IOrderRepository
            var orderRepository = Substitute.For<IOrderRepository>();
            orderRepository.GetById(orderId).Returns(new Order(orderId, orderStatus, "testuser"));

            // Create an instance of the ReprocessOrderCommand
            var command = new ReprocessOrderCommand(orderRepository, orderId, publisher);

            // Act
            var canExecute = await command.CanExecute();

            // Assert
            canExecute.Should().BeTrue();
        }

        [Fact]
        public async Task CanExecute_Should_ReturnFalse_IfOrderStatusIsNotPaymentFailed()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var orderStatus = OrderStatus.Created;

            // Create a mock for IMessagePublisher<ReprocessOrderDto>
            var publisher = Substitute.For<IMessagePublisher<ReprocessOrderDto>>();

            // Create a mock for IOrderRepository
            var orderRepository = Substitute.For<IOrderRepository>();
            orderRepository.GetById(orderId).Returns(new Order(orderId, orderStatus, "testuser"));

            // Create an instance of the ReprocessOrderCommand
            var command = new ReprocessOrderCommand(orderRepository, orderId, publisher);

            // Act
            var canExecute = await command.CanExecute();

            // Assert
            canExecute.Should().BeFalse();
        }
    }
}