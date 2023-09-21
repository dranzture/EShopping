using AutoMapper;
using Grpc.Core;
using NSubstitute;
using InventoryService.Core.Commands.InventoryCommands;
using InventoryService.Core.Dtos;
using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;
using InventoryCoreService = InventoryService.Core.Services.InventoryService;

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
        var inventoryDto = new InventoryDto
        {
            Name = "TestItem",
            Description = "Test description",
            InStock = 10,
            Height = 5,
            Width = 3,
            Weight = 2,
            Price = 100
        };
        var username = "TestUser";
        var cancellationToken = CancellationToken.None;

        var addInventoryCommand = Substitute.For<AddInventoryCommand>(_repository, Arg.Any<Inventory>());
        addInventoryCommand.CanExecute().Returns(true);
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
        var inventoryDto = new InventoryDto
        {
            Name = "TestItem",
            Description = "Test description",
            InStock = 10,
            Height = 5,
            Width = 3,
            Weight = 2,
            Price = 100
        };
        var username = "TestUser";
        var cancellationToken = CancellationToken.None;

        var addInventoryCommand = Substitute.For<AddInventoryCommand>(_repository, Arg.Any<Inventory>());
        addInventoryCommand.CanExecute().Returns(false);
        _mapper.Map<Inventory>(inventoryDto).Returns(new Inventory());

        // Act & Assert
        await Assert.ThrowsAsync<RpcException>(() =>
            _inventoryService.AddInventory(inventoryDto, username, cancellationToken));
    }

    [Fact]
    public async Task UpdateInventory_ValidInput_Success()
    {
        // Arrange
        var inventoryDto = new InventoryDto
        {
            Name = "UpdatedItem",
            Description = "Updated description",
            InStock = 15,
            Height = 6,
            Width = 4,
            Weight = 3,
            Price = 120,
            Id = Guid.NewGuid()
        };
        var username = "TestUser";
        var cancellationToken = CancellationToken.None;

        var updateInventoryCommand =
            Substitute.For<UpdateInventoryCommand>(_repository, Arg.Any<Inventory>(), username);
        updateInventoryCommand.CanExecute().Returns(true);
        _mapper.Map<Inventory>(inventoryDto).Returns(new Inventory());

        // Act
        await _inventoryService.UpdateInventory(inventoryDto, username, cancellationToken);

        // Assert: No exceptions are thrown for a successful update
    }

    [Fact]
    public async Task UpdateInventory_InvalidInput_ThrowsRpcException()
    {
        // Arrange
        var inventoryDto = new InventoryDto
        {
            Name = "UpdatedItem",
            Description = "Updated description",
            InStock = 15,
            Height = 6,
            Width = 4,
            Weight = 3,
            Price = 120,
            Id = Guid.NewGuid()
        };
        var username = "TestUser";
        var cancellationToken = CancellationToken.None;

        var updateInventoryCommand =
            Substitute.For<UpdateInventoryCommand>(_repository, Arg.Any<Inventory>(), username);
        updateInventoryCommand.CanExecute().Returns(false);
        _mapper.Map<Inventory>(inventoryDto).Returns(new Inventory());

        // Act & Assert
        await Assert.ThrowsAsync<RpcException>(() =>
            _inventoryService.UpdateInventory(inventoryDto, username, cancellationToken));
    }

    [Fact]
    public async Task DeleteInventory_ValidInput_Success()
    {
        // Arrange
        var id = Guid.NewGuid();
        var username = "TestUser";
        var cancellationToken = CancellationToken.None;

        var deleteInventoryCommand = Substitute.For<DeleteInventoryCommand>(_repository, id, username);
        deleteInventoryCommand.CanExecute().Returns(true);

        // Act
        await _inventoryService.DeleteInventory(id, username, cancellationToken);

        // Assert: No exceptions are thrown for a successful deletion
    }

    [Fact]
    public async Task DeleteInventory_InvalidInput_ThrowsRpcException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var username = "TestUser";
        var cancellationToken = CancellationToken.None;

        var deleteInventoryCommand = Substitute.For<DeleteInventoryCommand>(_repository, id, username);
        deleteInventoryCommand.CanExecute().Returns(false);

        // Act & Assert
        await Assert.ThrowsAsync<RpcException>(() =>
            _inventoryService.DeleteInventory(id, username, cancellationToken));
    }

    [Fact]
    public async Task IncreaseInventory_ValidInput_Success()
    {
        // Arrange
        var id = Guid.NewGuid();
        var amount = 5;
        var username = "TestUser";
        var cancellationToken = CancellationToken.None;

        var increaseInventoryCommand = Substitute.For<IncreaseInventoryCommand>(_repository, id, amount, username);
        increaseInventoryCommand.CanExecute().Returns(true);

        // Act
        await _inventoryService.IncreaseInventory(id, amount, username, cancellationToken);

        // Assert: No exceptions are thrown for a successful increase
    }

    [Fact]
    public async Task IncreaseInventory_InvalidInput_ThrowsRpcException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var amount = -5; // Invalid amount
        var username = "TestUser";
        var cancellationToken = CancellationToken.None;

        var increaseInventoryCommand = Substitute.For<IncreaseInventoryCommand>(_repository, id, amount, username);
        increaseInventoryCommand.CanExecute().Returns(false);

        // Act & Assert
        await Assert.ThrowsAsync<RpcException>(() =>
            _inventoryService.IncreaseInventory(id, amount, username, cancellationToken));
    }

    [Fact]
    public async Task DecreaseInventory_ValidInput_Success()
    {
        // Arrange
        var id = Guid.NewGuid();
        var amount = 5;
        var username = "TestUser";
        var cancellationToken = CancellationToken.None;

        var decreaseInventoryCommand = Substitute.For<DecreaseInventoryCommand>(_repository, id, amount, username);
        decreaseInventoryCommand.CanExecute().Returns(true);

        // Act
        await _inventoryService.DecreaseInventory(id, amount, username, cancellationToken);

        // Assert: No exceptions are thrown for a successful decrease
    }

    [Fact]
    public async Task DecreaseInventory_InvalidInput_ThrowsRpcException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var amount = -5; // Invalid amount
        var username = "TestUser";
        var cancellationToken = CancellationToken.None;

        var decreaseInventoryCommand = Substitute.For<DecreaseInventoryCommand>(_repository, id, amount, username);
        decreaseInventoryCommand.CanExecute().Returns(false);

        // Act & Assert
        await Assert.ThrowsAsync<RpcException>(() =>
            _inventoryService.DecreaseInventory(id, amount, username, cancellationToken));
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
            new Guid() // Id
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
        var name = "TestItem";
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