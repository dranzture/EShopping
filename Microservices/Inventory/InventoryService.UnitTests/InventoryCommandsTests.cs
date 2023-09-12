﻿using InventoryService.Core.Commands.InventoryCommands;
using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;
using NSubstitute;

namespace InventoryService.Tests;

public class InventoryCommandsTests
{
    private readonly Guid _guid = new();
    private readonly IInventoryRepository _repository = Substitute.For<IInventoryRepository>();

    [Fact]
    public void AddInventoryCommand_CanExecute_Should_Return_True()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod", 5, 10.0M, 5.0M, 20, "dranzture");
        _repository.GetByName("TestInv")
            .Returns((Inventory?)null);
        var command = new AddInventoryCommand(_repository, inventory);

        //Act

        var result = command.CanExecute();

        //Assert

        Assert.True(result);
    }

    [Fact]
    public void AddInventoryCommand_CanExecute_Should_Return_False_When_Duplicate_Found()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod", 5, 10.0M, 5.0M, 20, "dranzture");
        _repository.GetByName("TestInv")
            .Returns(inventory);

        var command = new AddInventoryCommand(_repository, inventory);

        //Act
        var result = command.CanExecute();

        //Assert
        Assert.False(result);
    }

    [Fact]
    public void DecreaseInventoryCommand_CanExecute_Should_Return_True()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod",
            5, 10.0M, 5.0M, 20, "dranzture", _guid);
        var username = "dranzure";
        _repository.GetById(_guid)
            .Returns(inventory);

        var command = new DecreaseInventoryCommand(_repository, _guid, 5, username);

        //Act
        var result = command.CanExecute();

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void DecreaseInventoryCommand_CanExecute_Should_Return_False_When_Not_Found()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod",
            5, 10.0M, 5.0M, 20, "dranzture", _guid);
        var username = "dranzure";
        _repository.GetByName("TestInv")
            .Returns((Inventory?)null);
        var command = new DecreaseInventoryCommand(_repository, _guid, 6, username);

        //Act
        var result = command.CanExecute();

        //Assert
        Assert.False(result);
    }

    [Fact]
    public void DecreaseInventoryCommand_CanExecute_Should_Return_False_When_Amount_Less_Than_Zero()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod",
            5, 10.0M, 5.0M, 20, "dranzture", _guid);
        var username = "dranzure";
        _repository.GetByName("TestInv")
            .Returns(inventory);

        var command = new DecreaseInventoryCommand(_repository, _guid, -1, username);

        //Act

        var result = command.CanExecute();

        //Assert

        Assert.False(result);
    }

    [Fact]
    public void IncreaseInventoryCommand_CanExecute_Should_Return_True()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod",
            5, 10.0M, 5.0M, 20, "dranzture", _guid);
        var username = "dranzure";
        _repository.GetById(_guid)
            .Returns(inventory);

        var command = new IncreaseInventoryCommand(_repository, _guid, 5, username);

        //Act

        var result = command.CanExecute();

        //Assert

        Assert.True(result);
    }

    [Fact]
    public void IncreaseInventoryCommand_CanExecute_Should_Return_False_When_Not_Found()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod",
            5, 10.0M, 5.0M, 20, "dranzture", _guid);
        var username = "dranzure";
        _repository.GetById(_guid)
            .Returns((Inventory?)null);
        var command = new IncreaseInventoryCommand(_repository, _guid, 6, username);

        //Act

        var result = command.CanExecute();

        //Assert

        Assert.False(result);
    }

    [Fact]
    public void IncreaseInventoryCommand_CanExecute_Should_Return_False_When_Amount_Less_Than_Zero()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod",
            5, 10.0M, 5.0M, 20, "dranzture", _guid);
        var username = "dranzure";
        _repository.GetById(_guid)
            .Returns(inventory);

        var command = new IncreaseInventoryCommand(_repository, _guid, -1, username);

        //Act

        var result = command.CanExecute();

        //Assert

        Assert.False(result);
    }

    [Fact]
    public void UpdateInventoryCommand_CanExecute_Should_Return_True()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod",
            5, 10.0M, 5.0M, 20, "dranzture", _guid);
        var username = "dranzure";
        _repository.GetById(_guid)
            .Returns(inventory);

        //Act
        var command = new UpdateInventoryCommand(_repository, inventory, username);

        var canExecute = command.CanExecute();

        //Assert

        Assert.True(canExecute);
    }

    [Fact]
    public void UpdateInventoryCommand_CanExecute_Should_Return_False_When_Not_Found()
    {
        //Arrange
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod",
            5, 10.0M, 5.0M, 20, "dranzture", _guid);
        var username = "dranzure";
        _repository.GetById(_guid)
            .Returns((Inventory?)null);

        //Act
        var command = new UpdateInventoryCommand(_repository, inventory, username);

        var canExecute = command.CanExecute();

        //Assert

        Assert.False(canExecute);
    }

    [Fact]
    public void DeleteInventoryCommand_CanExecute_Should_Return_True()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod",
            5, 10.0M, 5.0M, 20, "dranzture", _guid);
        var username = "dranzure";
        _repository.GetById(_guid)
            .Returns(inventory);

        //Act
        var command = new DeleteInventoryCommand(_repository, _guid);

        var canExecute = command.CanExecute();

        //Assert

        Assert.True(canExecute);
    }

    [Fact]
    public void DeleteInventoryCommand_CanExecute_Should_Return_False_When_Not_Found()
    {
        //Arrange
        var inventory = new Inventory("TestInv", "TestingThisMethod",
            5, 10.0M, 5.0M, 20, "dranzture", _guid);
        var username = "dranzure";
        _repository.GetById(_guid)
            .Returns((Inventory?)null);

        //Act
        var command = new DeleteInventoryCommand(_repository, _guid);

        var canExecute = command.CanExecute();

        //Assert

        Assert.False(canExecute);
    }
}