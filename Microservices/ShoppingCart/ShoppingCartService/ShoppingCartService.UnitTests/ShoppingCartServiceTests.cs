
using AutoMapper;
using NSubstitute;
using ShoppingCartService.Core.Dtos;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.Models;
using Xunit;

public class ShoppingCartServiceTests
{
    private readonly IMapper _mapper;
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IPublisher<string, ShoppingCart> _publisher;
    private readonly ShoppingCartService.Core.Services.ShoppingCartService _shoppingCartService;

    public ShoppingCartServiceTests()
    {
        _mapper = Substitute.For<IMapper>();
        _shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
        _publisher = Substitute.For<IPublisher<string, ShoppingCart>>();
        _shoppingCartService = new ShoppingCartService.Core.Services.ShoppingCartService(
            _shoppingCartRepository,
            _mapper,
            _publisher
        );
    }

    [Fact]
    public async Task AddShoppingCart_ValidInput_Success()
    {
        // Arrange
        var shoppingCartDto = new ShoppingCartDto
        {
            Id = Guid.NewGuid(),
            Username = "TestUser",
            ShoppingItems = new List<ShoppingItemDto>
            {
                new ShoppingItemDto
                {
                    InventoryId = Guid.NewGuid(),
                    Amount = 5,
                    Price = 10.0m,
                    AddedDateTime = DateTimeOffset.UtcNow
                }
            }
        };
        var username = "TestUser";
        var cancellationToken = CancellationToken.None;

        // Mock the necessary dependencies and commands as needed

        // Act
        var result = await _shoppingCartService.AddShoppingCart(shoppingCartDto, username, cancellationToken);

        // Assert
        // Add assertions based on the expected behavior
    }

    [Fact]
    public async Task AddShoppingItem_ValidInput_Success()
    {
        // Arrange
        var shoppingCartDto = new ShoppingCartDto
        {
            Id = Guid.NewGuid(),
            Username = "TestUser",
            ShoppingItems = new List<ShoppingItemDto>
            {
                new ShoppingItemDto
                {
                    InventoryId = Guid.NewGuid(),
                    Amount = 5,
                    Price = 10.0m,
                    AddedDateTime = DateTimeOffset.UtcNow
                }
            }
        };
        var inventoryDto = new InventoryDto
        {
            Id = Guid.NewGuid(),
            Name = "TestItem",
            Description = "Test Description",
            InStock = 10,
            Height = 5.0m,
            Width = 3.0m,
            Weight = 2.0m,
            Price = 15.0m
        };
        var amount = 3;
        var username = "TestUser";
        var cancellationToken = CancellationToken.None;

        // Mock the necessary dependencies and commands as needed

        // Act
        await _shoppingCartService.AddShoppingItem(shoppingCartDto, inventoryDto, amount, username, cancellationToken);

        // Assert
        // Add assertions based on the expected behavior
    }

    [Fact]
    public async Task UpdateShoppingItem_ValidInput_Success()
    {
        // Arrange
        var shoppingCartDto = new ShoppingCartDto
        {
            Id = Guid.NewGuid(),
            Username = "TestUser",
            ShoppingItems = new List<ShoppingItemDto>
            {
                new ShoppingItemDto
                {
                    InventoryId = Guid.NewGuid(),
                    Amount = 5,
                    Price = 10.0m,
                    AddedDateTime = DateTimeOffset.UtcNow
                }
            }
        };
        var inventoryDto = new InventoryDto
        {
            Id = Guid.NewGuid(),
            Name = "TestItem",
            Description = "Test Description",
            InStock = 10,
            Height = 5.0m,
            Width = 3.0m,
            Weight = 2.0m,
            Price = 15.0m
        };
        var amount = 3;
        var username = "TestUser";
        var cancellationToken = CancellationToken.None;

        // Mock the necessary dependencies and commands as needed

        // Act
        await _shoppingCartService.UpdateShoppingItem(shoppingCartDto, inventoryDto, amount, username, cancellationToken);

        // Assert
        // Add assertions based on the expected behavior
    }

    [Fact]
    public async Task DeleteShoppingItem_ValidInput_Success()
    {
        // Arrange
        var shoppingCartDto = new ShoppingCartDto
        {
            Id = Guid.NewGuid(),
            Username = "TestUser",
            ShoppingItems = new List<ShoppingItemDto>
            {
                new ShoppingItemDto
                {
                    InventoryId = Guid.NewGuid(),
                    Amount = 5,
                    Price = 10.0m,
                    AddedDateTime = DateTimeOffset.UtcNow
                }
            }
        };
        var inventoryDto = new InventoryDto
        {
            Id = Guid.NewGuid(),
            Name = "TestItem",
            Description = "Test Description",
            InStock = 10,
            Height = 5.0m,
            Width = 3.0m,
            Weight = 2.0m,
            Price = 15.0m
        };
        var username = "TestUser";
        var cancellationToken = CancellationToken.None;

        // Mock the necessary dependencies and commands as needed

        // Act
        await _shoppingCartService.DeleteShoppingItem(shoppingCartDto, inventoryDto, username, cancellationToken);

        // Assert
        // Add assertions based on the expected behavior
    }

    [Fact]
    public async Task CheckoutShoppingCart_ValidInput_Success()
    {
        // Arrange
        var shoppingCartDto = new ShoppingCartDto
        {
            Id = Guid.NewGuid(),
            Username = "TestUser",
            ShoppingItems = new List<ShoppingItemDto>
            {
                new ShoppingItemDto
                {
                    InventoryId = Guid.NewGuid(),
                    Amount = 5,
                    Price = 10.0m,
                    AddedDateTime = DateTimeOffset.UtcNow
                }
            }
        };
        var username = "TestUser";
        var cancellationToken = CancellationToken.None;

        // Mock the necessary dependencies and commands as needed

        // Act
        await _shoppingCartService.CheckoutShoppingCart(shoppingCartDto, username, cancellationToken);

        // Assert
        // Add assertions based on the expected behavior
    }

    [Fact]
    public async Task GetOrderDetails_ValidInput_Success()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var cancellationToken = CancellationToken.None;
        var shoppingCart = new ShoppingCart( "TestUser", cartId);

        _shoppingCartRepository.GetShoppingCartById(cartId, cancellationToken)
            .Returns(Task.FromResult(shoppingCart));

        // Act
        var result = await _shoppingCartService.GetOrderDetails(cartId, cancellationToken);

        // Assert
        Assert.Equal(shoppingCart.Id, result.Id);
        Assert.Equal(shoppingCart.Username , result.Username);
        Assert.Equal(shoppingCart.Status ,result.Status);
    }
}
