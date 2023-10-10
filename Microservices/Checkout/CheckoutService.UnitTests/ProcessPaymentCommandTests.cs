using CheckoutService.Core.Commands;
using CheckoutService.Core.Dtos;
using CheckoutService.Core.Entities;
using CheckoutService.Core.ValueObjects;
using NSubstitute;

namespace CheckoutService.UnitTests;

public class CommandTests
{
    [Fact]
    public async Task ProcessPaymentCommand_CanExecute_Should_ReturnTrue_WithValidCreditCardAndItems()
    {
        // Arrange
        var validCreditCard = new CreditCard("testuser", 1234567890123456UL, isDefault: true);

        var shoppingItems = new List<ShoppingItemDto>
        {
            new ShoppingItemDto { TotalPrice = 10 },
            new ShoppingItemDto { TotalPrice = 20 },
            // Add more items as needed
        };

        var shoppingCartDto = new ShoppingCartDto
        {
            ShoppingItems = shoppingItems,
            // Set other properties as needed
        };

        var command = new ProcessPaymentCommand(validCreditCard, shoppingCartDto);

        // Act
        var canExecute = await command.CanExecute();

        // Assert
        Assert.True(canExecute);
    }

    [Fact]
    public async Task ProcessPaymentCommand_CanExecute_Should_ReturnFalse_WithInvalidCreditCard()
    {
        // Arrange
        var invalidCreditCard = new CreditCard("testuser", 1234UL, isDefault: true);

        var shoppingItems = new List<ShoppingItemDto>
        {
            new ShoppingItemDto { TotalPrice = 10 },
            new ShoppingItemDto { TotalPrice = 20 },
            // Add more items as needed
        };

        var shoppingCartDto = new ShoppingCartDto
        {
            ShoppingItems = shoppingItems,
            // Set other properties as needed
        };

        var command = new ProcessPaymentCommand(invalidCreditCard, shoppingCartDto);

        // Act
        var canExecute = await command.CanExecute();

        // Assert
        Assert.False(canExecute);
    }

    [Fact]
    public async Task ProcessPaymentCommand_CanExecute_Should_ReturnFalse_WithNoItems()
    {
        // Arrange
        var validCreditCard = new CreditCard("testuser", 1234567890123456UL, isDefault: true);

        var emptyShoppingCartDto = new ShoppingCartDto
        {
            ShoppingItems = new List<ShoppingItemDto>(), // Empty list
            // Set other properties as needed
        };

        var command = new ProcessPaymentCommand(validCreditCard, emptyShoppingCartDto);

        // Act
        var canExecute = await command.CanExecute();

        // Assert
        Assert.False(canExecute);
    }

    [Fact]
    public async Task ProcessPaymentCommand_Execute_Should_PrintPaymentInformation()
    {
        // Arrange
        var validCreditCard = new CreditCard("testuser", 1234567890123456UL, isDefault: true);

        var shoppingItems = new List<ShoppingItemDto>
        {
            new ShoppingItemDto { TotalPrice = 10 },
            new ShoppingItemDto { TotalPrice = 20 },
            // Add more items as needed
        };

        var shoppingCartDto = new ShoppingCartDto
        {
            ShoppingItems = shoppingItems,
            // Set other properties as needed
        };

        var command = new ProcessPaymentCommand(validCreditCard, shoppingCartDto);

        // Capture the console output
        using var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        // Act
        await command.Execute();

        // Assert
        var capturedOutput = consoleOutput.ToString();
        Assert.Contains("Using credit card: 1234567890123456", capturedOutput);
        Assert.Contains("Processing payment", capturedOutput);
        Assert.Contains("Payment processed", capturedOutput);
    }
}