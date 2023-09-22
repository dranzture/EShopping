using AutoMapper;
using Grpc.Core;
using InventoryService.Core.Commands;
using NSubstitute;
using InventoryService.Core.Commands.InventoryCommands;
using InventoryService.Core.Dtos;
using InventoryService.Core.Entities;
using InventoryService.Core.Interfaces;
using NSubstitute.ReceivedExtensions;
using InventoryCoreService = InventoryService.Core.Services.InventoryService;
namespace InventoryService.Tests;
public class InventoryServiceTests
{
    private readonly IMapper _mapper;
    private readonly IInventoryRepository _repository;
    private readonly  InventoryCoreService _inventoryService;

    public InventoryServiceTests()
    {
        _mapper = Substitute.For<IMapper>();
        _repository = Substitute.For<IInventoryRepository>();
        _inventoryService = new InventoryCoreService(_repository, _mapper);
    }

    [Fact]
    public async Task AddInventory_ValidInput_Success()
    {
        // Arrange
        var inventoryId = Guid.NewGuid();
        var username = "testuser";
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
        
        var cancellationToken = CancellationToken.None;

        _repository.GetByName(Arg.Any<string>()).Returns((Inventory?)null);
        _mapper.Map<Inventory>(inventoryDto).Returns(new Inventory());

        // Act
        var result = await _inventoryService.AddInventory(inventoryDto, username, cancellationToken);

        // Assert
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task AddInventory_DuplicateInventory_ThrowsRpcException()
    {
        // Arrange
        var inventoryId = Guid.NewGuid();
        var username = "testuser";
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
        
        var cancellationToken = CancellationToken.None;

        _repository.GetByName(Arg.Any<string>()).Returns(inventory);
        _mapper.Map<Inventory>(inventoryDto).Returns(inventory);

        // Act & Assert
        await Assert.ThrowsAsync<RpcException>(() =>
            _inventoryService.AddInventory(inventoryDto, username, cancellationToken));
    }

    [Fact]
    public async Task UpdateInventory_ValidInput_Success()
    {
        // Arrange
        var inventoryId = Guid.NewGuid();
        var username = "testuser";
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
        
        var cancellationToken = CancellationToken.None;

        _repository.GetById(Arg.Any<Guid>()).Returns(inventory);
        _mapper.Map<Inventory>(inventoryDto).Returns(inventory);

        // Act
        await _inventoryService.UpdateInventory(inventoryDto, username, cancellationToken);

        //Assert
        await _repository.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task UpdateInventory_InvalidInput_ThrowsRpcException()
    {
// Arrange
        var inventoryId = Guid.NewGuid();
        var username = "testuser";
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
        
        var cancellationToken = CancellationToken.None;

        _repository.GetById(Arg.Any<Guid>()).Returns((Inventory?)null);
        _mapper.Map<Inventory>(inventoryDto).Returns(inventory);

        // Act & Assert
        await Assert.ThrowsAsync<RpcException>(() =>
            _inventoryService.UpdateInventory(inventoryDto, username, cancellationToken));
    }

    [Fact]
    public async Task DeleteInventory_ValidInput_Success()
    {
        // Arrange
        var inventoryId = Guid.NewGuid();
        var username = "testuser";
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
        
        var cancellationToken = CancellationToken.None;

        _repository.GetById(Arg.Any<Guid>()).Returns(inventory);
        _mapper.Map<Inventory>(inventoryDto).Returns(inventory);
        // Act
        await _inventoryService.DeleteInventory(inventoryDto, username, cancellationToken);

        // Assert: No exceptions are thrown for a successful deletion
        await _repository.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task DeleteInventory_InvalidInput_ThrowsRpcException()
    {
        // Arrange
        var inventoryId = Guid.NewGuid();
        var username = "testuser";
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
        
        var cancellationToken = CancellationToken.None;

        _repository.GetById(Arg.Any<Guid>()).Returns((Inventory?)null);
        _mapper.Map<Inventory>(inventoryDto).Returns(inventory);

        // Act & Assert
        await Assert.ThrowsAsync<RpcException>(() =>
            _inventoryService.DeleteInventory(inventoryDto, username, cancellationToken));
    }

    [Fact]
    public async Task IncreaseInventory_ValidInput_Success()
    {
        // Arrange
        var inventoryId = Guid.NewGuid();
        var username = "testuser";
        int amount = 5;
        var inventoryDto = new InventoryDto
        {
            Id = inventoryId,
            Description = "test",
            Height = 1,
            Width = 1,
            Weight = 1,
            Price = 1,
            Name = "test",
            InStock = 3
            // Initialize with necessary data using object initializer
        };
        var inventory = new Inventory(inventoryDto.Name,
            inventoryDto.Description, inventoryDto.InStock, inventoryDto.Height, inventoryDto.Width,
            inventoryDto.Weight, inventoryDto.Price, username, inventoryId);
        
        var cancellationToken = CancellationToken.None;

        _repository.GetById(Arg.Any<Guid>()).Returns(inventory);
        _mapper.Map<Inventory>(inventoryDto).Returns(inventory);

        // Act
        await _inventoryService.IncreaseInventory(inventoryDto, amount, username);

        // Assert: No exceptions are thrown for a successful increase
        await _repository.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task IncreaseInventory_InvalidInput_ThrowsRpcException()
    {
        var inventoryId = Guid.NewGuid();
        var username = "testuser";
        int amount = -5;
        var inventoryDto = new InventoryDto
        {
            Id = inventoryId,
            Description = "test",
            Height = 1,
            Width = 1,
            Weight = 1,
            Price = 1,
            Name = "test",
            InStock = 3
            // Initialize with necessary data using object initializer
        };
        var inventory = new Inventory(inventoryDto.Name,
            inventoryDto.Description, inventoryDto.InStock, inventoryDto.Height, inventoryDto.Width,
            inventoryDto.Weight, inventoryDto.Price, username, inventoryId);
        
        var cancellationToken = CancellationToken.None;

        _repository.GetById(Arg.Any<Guid>()).Returns(inventory);
        _mapper.Map<Inventory>(inventoryDto).Returns(inventory);
        
        // Act & Assert
        await Assert.ThrowsAsync<RpcException>(() =>
            _inventoryService.IncreaseInventory(inventoryDto, amount, username, cancellationToken));
    }

    [Fact]
    public async Task DecreaseInventory_ValidInput_Success()
    {
        // Arrange
        var inventoryId = Guid.NewGuid();
        var username = "testuser";
        int amount = 5;
        var inventoryDto = new InventoryDto
        {
            Id = inventoryId,
            Description = "test",
            Height = 1,
            Width = 1,
            Weight = 1,
            Price = 1,
            Name = "test",
            InStock = 10
            // Initialize with necessary data using object initializer
        };
        var inventory = new Inventory(inventoryDto.Name,
            inventoryDto.Description, inventoryDto.InStock, inventoryDto.Height, inventoryDto.Width,
            inventoryDto.Weight, inventoryDto.Price, username, inventoryId);
        

        // Act
        await _inventoryService.DecreaseInventory(inventoryDto, amount, username);

        // Assert: No exceptions are thrown for a successful decrease
    }

    [Fact]
    public async Task DecreaseInventory_InvalidInput_ThrowsRpcException()
    {
        // Arrange
        var inventoryId = Guid.NewGuid();
        var username = "testuser";
        int amount = 5;
        var inventoryDto = new InventoryDto
        {
            Id = inventoryId,
            Description = "test",
            Height = 1,
            Width = 1,
            Weight = 1,
            Price = 1,
            Name = "test",
            InStock = 3
            // Initialize with necessary data using object initializer
        };
        var inventory = new Inventory(inventoryDto.Name,
            inventoryDto.Description, inventoryDto.InStock, inventoryDto.Height, inventoryDto.Width,
            inventoryDto.Weight, inventoryDto.Price, username, inventoryId);
        
        var cancellationToken = CancellationToken.None;

        _repository.GetByName(Arg.Any<string>()).Returns(inventory);
        _mapper.Map<Inventory>(inventoryDto).Returns(new Inventory());

        // Act & Assert
        await Assert.ThrowsAsync<RpcException>(() =>
            _inventoryService.DecreaseInventory(inventoryDto, amount, username, cancellationToken));
    }
    [Fact]
    public async Task DecreaseInventory_WhenNotFound_ThrowsRpcException()
    {
        // Arrange
        var inventoryId = Guid.NewGuid();
        var username = "testuser";
        int amount = -5;
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
        
        var cancellationToken = CancellationToken.None;

        _repository.GetByName(Arg.Any<string>()).Returns((Inventory?)null);
        _mapper.Map<Inventory>(inventoryDto).Returns(new Inventory());

        // Act & Assert
        await Assert.ThrowsAsync<RpcException>(() =>
            _inventoryService.DecreaseInventory(inventoryDto, amount, username, cancellationToken));
    }
    [Fact]
    public async Task GetAllInventory_ValidInput_Success()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var inventorySet = new HashSet<Inventory>
        {
            new Inventory(
                "TestItem1", // Name
                "Test description", // Description
                10, // InStock
                5, // Height
                3, // Width
                2, // Weight
                100, // Price
                "TestUser", // Username
                new Guid() // Id
            ),
            new Inventory(
                "TestItem2", // Name
                "Test description", // Description
                10, // InStock
                5, // Height
                3, // Width
                2, // Weight
                100, // Price
                "TestUser", // Username
                new Guid() // Id
            )
        };
        _repository.GetAllInventory(cancellationToken).Returns(inventorySet);

        // Act
        var result = await _inventoryService.GetAllInventory(cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(inventorySet.Count, result.Count);
    }

    [Fact]
    public async Task GetById_ValidId_Success()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cancellationToken = CancellationToken.None;
        var inventory = new Inventory(
            "TestItem1", // Name
            "Test description", // Description
            10, // InStock
            5, // Height
            3, // Width
            2, // Weight
            100, // Price
            "TestUser", // Username
            id // Id
        );
        _repository.GetById(id, cancellationToken).Returns(inventory);

        // Act
        var result = await _inventoryService.GetById(id, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
    }

    [Fact]
    public async Task GetById_InvalidId_ReturnsNull()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cancellationToken = CancellationToken.None;
        _repository.GetById(id, cancellationToken).Returns((Inventory)null);

        // Act
        var result = await _inventoryService.GetById(id, cancellationToken);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByName_ValidName_Success()
    {
        // Arrange
        var name = "TestItem1";
        var cancellationToken = CancellationToken.None;
        var inventory = new Inventory(
            "TestItem1", // Name
            "Test description", // Description
            10, // InStock
            5, // Height
            3, // Width
            2, // Weight
            100, // Price
            "TestUser", // Username
            new Guid() // Id
        );
        _repository.GetByName(name, cancellationToken).Returns(inventory);

        // Act
        var result = await _inventoryService.GetByName(name, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(name, result.Name);
    }

    [Fact]
    public async Task GetByName_InvalidName_ReturnsNull()
    {
        // Arrange
        var name = "NonExistentItem";
        var cancellationToken = CancellationToken.None;
        _repository.GetByName(name, cancellationToken).Returns((Inventory)null);

        // Act
        var result = await _inventoryService.GetByName(name, cancellationToken);

        // Assert
        Assert.Null(result);
    }
}