using AutoMapper;
using Grpc.Core;
using NSubstitute;
using ShoppingCartService.Core.Commands;
using ShoppingCartService.Core.Dtos;
using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.Extensions;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.Models;
using ShoppingCartService.Core.ValueObjects;

namespace ShoppingCartService.UnitTests;

public class ShoppingServiceTests
{
    [Fact]
    public async Task AddShoppingCart_ReturnsId_WhenCommandSucceeds()
    {
        // Arrange
        var shoppingCartDto = new ShoppingCartDto
        {
            // Initialize with necessary data using object initializer
            Username = "testuser",
            ShoppingItems = new List<ShoppingItemDto>
            {
                /* Initialize shopping items if needed */
            }
        };
        var username = "testuser";
        var cartId = Guid.NewGuid();

        var shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
        shoppingCartRepository.GetShoppingCartById(cartId).Returns((ShoppingCart)null);
        shoppingCartRepository.AddAsync(Arg.Any<ShoppingCart>()).Returns(Task.CompletedTask);
        shoppingCartRepository.SaveChangesAsync().Returns(true);

        var mapper = Substitute.For<IMapper>();
        var publisher = Substitute.For<IPublisher>();
        var service = new Core.Services.ShoppingCartService(shoppingCartRepository, mapper, publisher);

        var shoppingCart = new ShoppingCart(username, cartId);
        mapper.Map<ShoppingCart>(Arg.Any<ShoppingCartDto>()).Returns(shoppingCart);

        var addShoppingCartCommand = Substitute.For<ICommand>();
        addShoppingCartCommand.CanExecute().Returns(Task.FromResult(true));

        // Act
        var result = await service.AddShoppingCart(shoppingCartDto, username);

        // Assert
        Assert.Equal(cartId.ToString(), result);
    }

    [Fact]
    public async Task AddShoppingCart_ThrowsException_WhenCommandFails()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var username = "testuser";

        var shoppingCartDto = new ShoppingCartDto
        {
            // Initialize with necessary data using object initializer
            Id = cartId,
            Username = username,
            ShoppingItems = new List<ShoppingItemDto>
            {
                /* Initialize shopping items if needed */
            }
        };


        var shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
        shoppingCartRepository.GetShoppingCartByUsername(username).Returns(new ShoppingCart(username, cartId));
        shoppingCartRepository.AddAsync(Arg.Any<ShoppingCart>()).Returns(Task.CompletedTask);
        shoppingCartRepository.SaveChangesAsync().Returns(true);

        var mapper = Substitute.For<IMapper>();
        var publisher = Substitute.For<IPublisher>();
        var service = new Core.Services.ShoppingCartService(shoppingCartRepository, mapper, publisher);

        var shoppingCart = new ShoppingCart(username, cartId);
        mapper.Map<ShoppingCart>(Arg.Any<ShoppingCartDto>()).Returns(shoppingCart);

        // Act & Assert
        await Assert.ThrowsAsync<RpcException>(() => service.AddShoppingCart(shoppingCartDto, username));
    }

    [Fact]
    public async Task AddShoppingItem_CallsExecute_WhenCommandCanExecute()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var inventoryId = Guid.NewGuid();
        var username = "testuser";
        var quantity = 3;
        var shoppingCartDto = new ShoppingCartDto
        {
            // Initialize with necessary data using object initializer
            Id = cartId,
            Username = username,
            ShoppingItems = new List<ShoppingItemDto>
            {
                /* Initialize shopping items if needed */
            }
        };
        var inventoryDto = new InventoryDto
        {
            Id = inventoryId,
            Description = "test",
            Height = 1,
            Width = 1,
            Weight = 1,
            Price = 1,
            Name = "test",
            // Initialize with necessary data using object initializer
        };
        var inventory = new Inventory(inventoryDto.Name,
            inventoryDto.Description, inventoryDto.InStock, inventoryDto.Height, inventoryDto.Width,
            inventoryDto.Weight, inventoryDto.Price, username, inventoryId);

        var shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
        shoppingCartRepository.GetShoppingCartByUsername(username).Returns(new ShoppingCart(username, cartId));
        shoppingCartRepository.SaveChangesAsync().Returns(true);
        shoppingCartRepository.GetShoppingCartById(Arg.Any<Guid>()).Returns(new ShoppingCart(username, cartId));
        var mapper = Substitute.For<IMapper>();

        var shoppingCart = new ShoppingCart(username, cartId);
        mapper.Map<ShoppingCart>(Arg.Any<ShoppingCartDto>()).Returns(shoppingCart);
        mapper.Map<Inventory>(Arg.Any<InventoryDto>()).Returns(inventory); 

        var publisher = Substitute.For<IPublisher>();

        var service = new Core.Services.ShoppingCartService(shoppingCartRepository, mapper, publisher);

        var addShoppingItemCommand = Substitute.For<ICommand>();
        addShoppingItemCommand.CanExecute().Returns(true);

        // Act
        await service.AddShoppingItem(cartId, inventoryDto, quantity, username);

        // Assert
        await shoppingCartRepository.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task AddShoppingItem_ThrowsException_WhenCommandFails()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var inventoryDto = new InventoryDto
        {
            // Initialize with necessary data using object initializer
        };
        var quantity = 3;
        var username = "testuser";

        var shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
        var mapper = Substitute.For<IMapper>();
        var publisher = Substitute.For<IPublisher>();

        var service = new Core.Services.ShoppingCartService(shoppingCartRepository, mapper, publisher);

        var addShoppingItemCommand = Substitute.For<ICommand>();
        addShoppingItemCommand.CanExecute().Returns(false);

        // Act & Assert
        await Assert.ThrowsAsync<RpcException>(() =>
            service.AddShoppingItem(guid, inventoryDto, quantity, username));
    }

    [Fact]
    public async Task UpdateShoppingItem_CallsExecute_WhenCommandCanExecute()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var inventoryId = Guid.NewGuid();
        var username = "testuser";
        var quantity = 3;

        var shoppingCartItemDto = new ShoppingItemDto()
        {
            ShoppingCartId = cartId,
            InventoryId = inventoryId,
            TotalPrice = 1,
            Quantity = 1,
        };
        
        var shoppingCartDto = new ShoppingCartDto
        {
            // Initialize with necessary data using object initializer
            Id = cartId,
            Username = username,
            ShoppingItems = new List<ShoppingItemDto>
            {
                /* Initialize shopping items if needed */
                shoppingCartItemDto
            }
        };
        
        var inventoryDto = new InventoryDto
        {
            Id = inventoryId,
            Description = "test",
            Height = 1,
            Width = 1,
            Weight = 1,
            Price = 1,
            Name = "test",
            // Initialize with necessary data using object initializer
        };
        
        var inventory = new Inventory(inventoryDto.Name,
            inventoryDto.Description, inventoryDto.InStock, inventoryDto.Height, inventoryDto.Width,
            inventoryDto.Weight, inventoryDto.Price, username, inventoryId);

        var shoppingCart = new ShoppingCart(username, cartId);

        var shoppingItem = new ShoppingItem(inventoryId, shoppingCart.Id, username);
        shoppingCart.AddItem(inventory, 1,"test");
        
        var shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
        shoppingCartRepository.GetShoppingCartByUsername(username).Returns(shoppingCart);
        shoppingCartRepository.SaveChangesAsync().Returns(true);
        shoppingCartRepository.GetShoppingCartById(Arg.Any<Guid>()).Returns(shoppingCart);
        var mapper = Substitute.For<IMapper>();

        mapper.Map<ShoppingCart>(Arg.Any<ShoppingCartDto>()).Returns(shoppingCart);
        mapper.Map<Inventory>(Arg.Any<InventoryDto>()).Returns(inventory); 
        mapper.Map<ShoppingItem>(Arg.Any<ShoppingItemDto>()).Returns(shoppingItem); 

        var publisher = Substitute.For<IPublisher>();
        
        var service = new Core.Services.ShoppingCartService(shoppingCartRepository, mapper, publisher);

        // Act
        await service.UpdateShoppingItem(cartId, inventoryDto, quantity, username);

        // Assert
        await shoppingCartRepository.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task UpdateShoppingItem_ThrowsException_WhenCommandFails()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var inventoryId = Guid.NewGuid();
        var username = "testuser";
        var quantity = 3;
        var shoppingCartDto = new ShoppingCartDto
        {
            // Initialize with necessary data using object initializer
            Id = cartId,
            Username = username,
            ShoppingItems = new List<ShoppingItemDto>
            {
                /* Initialize shopping items if needed */
            }
        };
        var inventoryDto = new InventoryDto
        {
            Id = inventoryId,
            Description = "test",
            Height = 1,
            Width = 1,
            Weight = 1,
            Price = 1,
            Name = "test",
            // Initialize with necessary data using object initializer
        };
        var inventory = new Inventory(inventoryDto.Name,
            inventoryDto.Description, inventoryDto.InStock, inventoryDto.Height, inventoryDto.Width,
            inventoryDto.Weight, inventoryDto.Price, username, inventoryId);

        var shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
        shoppingCartRepository.GetShoppingCartByUsername(username).Returns(new ShoppingCart(username, cartId));
        shoppingCartRepository.SaveChangesAsync().Returns(true);
        shoppingCartRepository.GetShoppingCartById(Arg.Any<Guid>()).Returns(new ShoppingCart(username, cartId));
        var mapper = Substitute.For<IMapper>();

        var shoppingCart = new ShoppingCart(username, cartId);
        mapper.Map<ShoppingCart>(Arg.Any<ShoppingCartDto>()).Returns(shoppingCart);
        mapper.Map<Inventory>(Arg.Any<InventoryDto>()).Returns(inventory); 

        var publisher = Substitute.For<IPublisher>();


        var service = new Core.Services.ShoppingCartService(shoppingCartRepository, mapper, publisher);

        var updateShoppingItemCommand = Substitute.For<ICommand>();
        updateShoppingItemCommand.CanExecute().Returns(false);

        // Act & Assert
        await Assert.ThrowsAsync<RpcException>(() =>
            service.UpdateShoppingItem(cartId, inventoryDto, quantity, username));
    }

    [Fact]
    public async Task DeleteShoppingItem_CallsExecute_WhenCommandCanExecute()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var inventoryId = Guid.NewGuid();
        var username = "testuser";
        var quantity = 3;

        var shoppingCartItemDto = new ShoppingItemDto()
        {
            ShoppingCartId = cartId,
            InventoryId = inventoryId,
            TotalPrice = 1,
            Quantity = 1,
        };
        
        var shoppingCartDto = new ShoppingCartDto
        {
            // Initialize with necessary data using object initializer
            Id = cartId,
            Username = username,
            ShoppingItems = new List<ShoppingItemDto>
            {
                /* Initialize shopping items if needed */
                shoppingCartItemDto
            }
        };
        
        var inventoryDto = new InventoryDto
        {
            Id = inventoryId,
            Description = "test",
            Height = 1,
            Width = 1,
            Weight = 1,
            Price = 1,
            Name = "test",
            // Initialize with necessary data using object initializer
        };
        
        var inventory = new Inventory(inventoryDto.Name,
            inventoryDto.Description, inventoryDto.InStock, inventoryDto.Height, inventoryDto.Width,
            inventoryDto.Weight, inventoryDto.Price, username, inventoryId);

        var shoppingCart = new ShoppingCart(username, cartId);

        var shoppingItem = new ShoppingItem(inventoryId, shoppingCart.Id, username);
        shoppingCart.AddItem(inventory, 1,"test");
        
        var shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
        shoppingCartRepository.GetShoppingCartByUsername(username).Returns(shoppingCart);
        shoppingCartRepository.SaveChangesAsync().Returns(true);
        shoppingCartRepository.GetShoppingCartById(Arg.Any<Guid>()).Returns(shoppingCart);
        var mapper = Substitute.For<IMapper>();

        mapper.Map<ShoppingCart>(Arg.Any<ShoppingCartDto>()).Returns(shoppingCart);
        mapper.Map<Inventory>(Arg.Any<InventoryDto>()).Returns(inventory); 
        mapper.Map<ShoppingItem>(Arg.Any<ShoppingItemDto>()).Returns(shoppingItem); 

        var publisher = Substitute.For<IPublisher>();


        var service = new Core.Services.ShoppingCartService(shoppingCartRepository, mapper, publisher);

        // Act
        await service.DeleteShoppingItem(cartId, inventoryDto, username);

        // Assert
        await shoppingCartRepository.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task DeleteShoppingItem_ThrowsException_WhenCommandFails()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var inventoryId = Guid.NewGuid();
        var username = "testuser";
        var quantity = 3;
        var shoppingCartDto = new ShoppingCartDto
        {
            // Initialize with necessary data using object initializer
            Id = cartId,
            Username = username,
            ShoppingItems = new List<ShoppingItemDto>
            {
                /* Initialize shopping items if needed */
            }
        };
        var inventoryDto = new InventoryDto
        {
            Id = inventoryId,
            Description = "test",
            Height = 1,
            Width = 1,
            Weight = 1,
            Price = 1,
            Name = "test",
            // Initialize with necessary data using object initializer
        };
        var inventory = new Inventory(inventoryDto.Name,
            inventoryDto.Description, inventoryDto.InStock, inventoryDto.Height, inventoryDto.Width,
            inventoryDto.Weight, inventoryDto.Price, username, inventoryId);

        var shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
        shoppingCartRepository.GetShoppingCartByUsername(username).Returns(new ShoppingCart(username, cartId));
        shoppingCartRepository.SaveChangesAsync().Returns(true);
        shoppingCartRepository.GetShoppingCartById(Arg.Any<Guid>()).Returns(new ShoppingCart(username, cartId));
        var mapper = Substitute.For<IMapper>();

        var shoppingCart = new ShoppingCart(username, cartId);
        mapper.Map<ShoppingCart>(Arg.Any<ShoppingCartDto>()).Returns(shoppingCart);
        mapper.Map<Inventory>(Arg.Any<InventoryDto>()).Returns(inventory); 

        var publisher = Substitute.For<IPublisher>();


        var service = new Core.Services.ShoppingCartService(shoppingCartRepository, mapper, publisher);

        var deleteShoppingItemCommand = Substitute.For<ICommand>();
        deleteShoppingItemCommand.CanExecute().Returns(false);

        // Act & Assert
        await Assert.ThrowsAsync<RpcException>(
            () => service.DeleteShoppingItem(cartId, inventoryDto, username));
    }

    [Fact]
    public async Task CheckoutShoppingCart_CallsExecute_WhenCommandCanExecute()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var inventoryId = Guid.NewGuid();
        var username = "testuser";
        var quantity = 3;

        var shoppingCartItemDto = new ShoppingItemDto()
        {
            ShoppingCartId = cartId,
            InventoryId = inventoryId,
            TotalPrice = 1,
            Quantity = 1,
        };
        
        var shoppingCartDto = new ShoppingCartDto
        {
            // Initialize with necessary data using object initializer
            Id = cartId,
            Username = username,
            ShoppingItems = new List<ShoppingItemDto>
            {
                /* Initialize shopping items if needed */
                shoppingCartItemDto
            }
        };
        
        var inventoryDto = new InventoryDto
        {
            Id = inventoryId,
            Description = "test",
            Height = 1,
            Width = 1,
            Weight = 1,
            Price = 1,
            Name = "test",
            // Initialize with necessary data using object initializer
        };
        
        var inventory = new Inventory(inventoryDto.Name,
            inventoryDto.Description, inventoryDto.InStock, inventoryDto.Height, inventoryDto.Width,
            inventoryDto.Weight, inventoryDto.Price, username, inventoryId);

        var shoppingCart = new ShoppingCart(username, cartId);

        var shoppingItem = new ShoppingItem(inventoryId, shoppingCart.Id, username);
        shoppingCart.AddItem(inventory, 1,"test");
        
        var shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
        shoppingCartRepository.GetShoppingCartByUsername(username).Returns(shoppingCart);
        shoppingCartRepository.SaveChangesAsync().Returns(true);
        shoppingCartRepository.GetShoppingCartById(Arg.Any<Guid>()).Returns(shoppingCart);
        var mapper = Substitute.For<IMapper>();

        mapper.Map<ShoppingCart>(Arg.Any<ShoppingCartDto>()).Returns(shoppingCart);
        mapper.Map<Inventory>(Arg.Any<InventoryDto>()).Returns(inventory); 
        mapper.Map<ShoppingItem>(Arg.Any<ShoppingItemDto>()).Returns(shoppingItem); 

        var publisher = Substitute.For<IPublisher>();


        var service = new Core.Services.ShoppingCartService(shoppingCartRepository, mapper, publisher);

        // Act
        await service.CheckoutShoppingCart(cartId);

        // Assert
        await shoppingCartRepository.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task CheckoutShoppingCart_ThrowsException_WhenCommandFails()
    {
                // Arrange
        var cartId = Guid.NewGuid();
        var inventoryId = Guid.NewGuid();
        var username = "testuser";
        var quantity = 3;

        var shoppingCartItemDto = new ShoppingItemDto()
        {
            ShoppingCartId = cartId,
            InventoryId = inventoryId,
            TotalPrice = 1,
            Quantity = 1,
        };
        
        var shoppingCartDto = new ShoppingCartDto
        {
            // Initialize with necessary data using object initializer
            Id = cartId,
            Username = username,
            ShoppingItems = new List<ShoppingItemDto>
            {
                /* Initialize shopping items if needed */
                shoppingCartItemDto
            }
        };
        
        var inventoryDto = new InventoryDto
        {
            Id = inventoryId,
            Description = "test",
            Height = 1,
            Width = 1,
            Weight = 1,
            Price = 1,
            Name = "test",
            // Initialize with necessary data using object initializer
        };
        
        var inventory = new Inventory(inventoryDto.Name,
            inventoryDto.Description, inventoryDto.InStock, inventoryDto.Height, inventoryDto.Width,
            inventoryDto.Weight, inventoryDto.Price, username, inventoryId);

        var shoppingCart = new ShoppingCart(username, cartId);

        var shoppingItem = new ShoppingItem(inventoryId, shoppingCart.Id, username);
        var shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
        shoppingCartRepository.GetShoppingCartByUsername(username).Returns(shoppingCart);
        shoppingCartRepository.SaveChangesAsync().Returns(true);
        shoppingCartRepository.GetShoppingCartById(Arg.Any<Guid>()).Returns(shoppingCart);
        var mapper = Substitute.For<IMapper>();

        mapper.Map<ShoppingCart>(Arg.Any<ShoppingCartDto>()).Returns(shoppingCart);
        mapper.Map<Inventory>(Arg.Any<InventoryDto>()).Returns(inventory); 
        mapper.Map<ShoppingItem>(Arg.Any<ShoppingItemDto>()).Returns(shoppingItem); 

        var publisher = Substitute.For<IPublisher>();


        var service = new Core.Services.ShoppingCartService(shoppingCartRepository, mapper, publisher);
        

        // Act & Assert
        await Assert.ThrowsAsync<RpcException>(() => service.CheckoutShoppingCart(cartId));
    }

    [Fact]
    public async Task GetOrderDetails_ReturnsDto_WhenCartExists()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var shoppingCartDto = new ShoppingCartDto
        {
            // Initialize with necessary data using object initializer
            Username = "testuser",
            ShoppingItems = new List<ShoppingItemDto>
            {
                /* Initialize shopping items if needed */
            }
        };

        var shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
        var mapper = Substitute.For<IMapper>();
        var publisher = Substitute.For<IPublisher>();

        var service = new Core.Services.ShoppingCartService(shoppingCartRepository, mapper, publisher);

        var shoppingCart = new ShoppingCart("testuser", cartId);
        mapper.Map<ShoppingCartDto>(Arg.Any<ShoppingCart>()).Returns(shoppingCartDto);

        shoppingCartRepository.GetShoppingCartById(cartId).Returns(shoppingCart);

        // Act
        var result = await service.GetOrderDetails(cartId);

        // Assert
        Assert.NotNull(result);
        // Add more assertions as needed to verify the result matches the expected DTO.
    }
}