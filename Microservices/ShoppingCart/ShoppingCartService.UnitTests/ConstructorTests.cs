using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.Models;
using ShoppingCartService.Core.ValueObjects;

namespace ShoppingCartService.UnitTests;

public class ConstructorTests
{
    [Fact]
    public void Inventory_Constructor_WithValidArguments_InitializesProperties()
    {
        // Arrange
        var name = "Test Product";
        var description = "Description of the product";
        var inStock = 10;
        var height = 5.0m;
        var width = 3.0m;
        var weight = 1.5m;
        var price = 25.99m;
        var username = "testuser";

        // Act
        var inventory = new Inventory(name, description, inStock, height, width, weight, price, username);

        // Assert
        Assert.Equal(name, inventory.Name);
        Assert.Equal(description, inventory.Description);
        Assert.Equal(inStock, inventory.InStock);
        Assert.Equal(height, inventory.Height);
        Assert.Equal(width, inventory.Width);
        Assert.Equal(weight, inventory.Weight);
        Assert.Equal(price, inventory.Price);
        Assert.Equal(username, inventory.CreatedBy);
        Assert.True(inventory.CreatedDateTime <= DateTimeOffset.Now);
    }

    [Fact]
    public void Inventory_Constructor_WithInvalidArguments_ThrowsException()
    {
        // Arrange
        var name = "";
        var description = "Description of the product";
        var inStock = -10;
        var height = -5.0m;
        var width = -3.0m;
        var weight = -1.5m;
        var price = -25.99m;
        var username = "testuser";

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            new Inventory(name, description, inStock, height, width, weight, price, username);
        });
    }
    [Fact]
    public void ShoppingCart_Constructor_WithValidArguments_InitializesProperties()
    {
        // Arrange
        var username = "testuser";

        // Act
        var shoppingCart = new ShoppingCart(username);

        // Assert
        Assert.Equal(username, shoppingCart.Username);
        Assert.Equal(username, shoppingCart.CreatedBy);
        Assert.True(shoppingCart.CreatedDateTime <= DateTimeOffset.Now);
        Assert.Empty(shoppingCart.ShoppingItems);
        Assert.Equal(CheckoutStatus.None, shoppingCart.Status);
    }
    
}