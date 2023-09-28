using InventoryService.Core.Commands;
using InventoryService.Core.Entities;
using InventoryService.Core.Interfaces;
using NSubstitute;

namespace InventoryService.Tests;

public class InventoryCommandsTests
{
    private readonly Guid _guid = new();
    private readonly IInventoryRepository _repository = Substitute.For<IInventoryRepository>();

    [Fact]
    public async Task AddInventoryCommand_CanExecute_Should_Return_True()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod",
            5, 10.0M, 5.0M, 20, 45, _guid);
        inventory.UpdateCreatedFields("dranzture");       
        _repository.GetByName("TestInv")
            .Returns((Inventory?)null);
        var command = new AddInventoryCommand(_repository, inventory, "dranzture");

        //Act

        var result = await command.CanExecute();

        //Assert

        Assert.True(result);
    }

    [Fact]
    public async Task AddInventoryCommand_CanExecute_Should_Return_False_When_Duplicate_Found()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod",
            5, 10.0M, 5.0M, 20, 45, _guid);
        inventory.UpdateCreatedFields("dranzture");       
        _repository.GetByName("TestInv")
            .Returns(inventory);

        var command = new AddInventoryCommand(_repository, inventory, "dranzture");

        //Act
        var result = await command.CanExecute();

        //Assert
        Assert.False(result);
    }

    [Fact]
    public async Task DecreaseInventoryCommand_CanExecute_Should_Return_True()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod",
            5, 10.0M, 5.0M, 20, 45, _guid);
        inventory.UpdateCreatedFields("dranzture");
        var username = "dranzure";
        _repository.GetById(_guid)
            .Returns(inventory);

        var command = new DecreaseInventoryCommand(_repository, inventory.Id, 5);

        //Act
        var result = await command.CanExecute();

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DecreaseInventoryCommand_CanExecute_Should_Return_False_When_Not_Found()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod",
            5, 10.0M, 5.0M, 20, 45, _guid);
        inventory.UpdateCreatedFields("dranzture");
        var username = "dranzure";
        _repository.GetByName("TestInv")
            .Returns((Inventory?)null);
        var command = new DecreaseInventoryCommand(_repository, inventory.Id, 6);

        //Act
        var result = await command.CanExecute();

        //Assert
        Assert.False(result);
    }

    [Fact]
    public async Task DecreaseInventoryCommand_CanExecute_Should_Return_False_When_Amount_Less_Than_Zero()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod",
            5, 10.0M, 5.0M, 20, 45, _guid);
        inventory.UpdateCreatedFields("dranzture");
        var username = "dranzure";
        _repository.GetByName("TestInv")
            .Returns(inventory);

        var command = new DecreaseInventoryCommand(_repository, inventory.Id,  -1);

        //Act

        var result = await command.CanExecute();

        //Assert

        Assert.False(result);
    }

    [Fact]
    public async Task IncreaseInventoryCommand_CanExecute_Should_Return_True()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod",
            5, 10.0M, 5.0M, 20, 45, _guid);
        inventory.UpdateCreatedFields("dranzture");
        var username = "dranzure";
        _repository.GetById(_guid)
            .Returns(inventory);

        var command = new IncreaseInventoryCommand(_repository, inventory.Id,  5);

        //Act

        var result = await command.CanExecute();

        //Assert

        Assert.True(result);
    }

    [Fact]
    public async Task IncreaseInventoryCommand_CanExecute_Should_Return_False_When_Not_Found()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod",
            5, 10.0M, 5.0M, 20, 45, _guid);
        inventory.UpdateCreatedFields("dranzture");
        var username = "dranzure";
        _repository.GetById(_guid)
            .Returns((Inventory?)null);
        var command = new IncreaseInventoryCommand(_repository, inventory.Id,  6);

        //Act

        var result = await command.CanExecute();

        //Assert

        Assert.False(result);
    }

    [Fact]
    public async Task IncreaseInventoryCommand_CanExecute_Should_Return_False_When_Amount_Less_Than_Zero()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod",
            5, 10.0M, 5.0M, 20, 45, _guid);
        inventory.UpdateCreatedFields("dranzture");
        var username = "dranzure";
        _repository.GetById(_guid)
            .Returns(inventory);

        var command = new IncreaseInventoryCommand(_repository, inventory.Id,  -1);

        //Act

        var result = await command.CanExecute();

        //Assert

        Assert.False(result);
    }

    [Fact]
    public async Task UpdateInventoryCommand_CanExecute_Should_Return_True()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod",
            5, 10.0M, 5.0M, 20, 45, _guid);
        inventory.UpdateCreatedFields("dranzture");
        var username = "dranzure";
        _repository.GetById(_guid)
            .Returns(inventory);

        //Act
        var command = new UpdateInventoryCommand(_repository, inventory, username);

        var canExecute = await command.CanExecute();

        //Assert

        Assert.True(canExecute);
    }

    [Fact]
    public async Task UpdateInventoryCommand_CanExecute_Should_Return_False_When_Not_Found()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod",
            5, 10.0M, 5.0M, 20, 45, _guid);
        inventory.UpdateCreatedFields("dranzture");
        var username = "dranzure";
        _repository.GetById(_guid)
            .Returns((Inventory?)null);

        //Act
        var command = new UpdateInventoryCommand(_repository, inventory, username);

        var canExecute = await command.CanExecute();

        //Assert

        Assert.False(canExecute);
    }

    [Fact]
    public async Task DeleteInventoryCommand_CanExecute_Should_Return_True()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod",
            5, 10.0M, 5.0M, 20, 45, _guid);
        inventory.UpdateCreatedFields("dranzture");
        var username = "dranzure";
        _repository.GetById(_guid)
            .Returns(inventory);

        //Act
        var command = new DeleteInventoryCommand(_repository, inventory, username);

        var canExecute = await command.CanExecute();

        //Assert

        Assert.True(canExecute);
    }

    [Fact]
    public async Task DeleteInventoryCommand_CanExecute_Should_Return_False_When_Not_Found()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod",
            5, 10.0M, 5.0M, 20, 45, _guid);
        inventory.UpdateCreatedFields("dranzture");
        var username = "dranzure";
        _repository.GetById(_guid)
            .Returns((Inventory?)null);

        //Act
        var command = new DeleteInventoryCommand(_repository, inventory, username);

        var canExecute = await command.CanExecute();

        //Assert

        Assert.False(canExecute);
    }
}