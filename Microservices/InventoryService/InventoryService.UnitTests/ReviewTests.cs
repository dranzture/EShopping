using InventoryService.Core.Commands.ReviewCommands;
using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;
using NSubstitute;

namespace InventoryService.Tests;

public class ReviewTests
{
    private readonly Guid _guid = new();
    private readonly IReviewRepository _repository = Substitute.For<IReviewRepository>();

    [Fact]
    public async void AddReviewCommand_CanExecute_Should_Return_True()
    {
        //Arrange
        var review = new Review(_guid, 1, "dranzture", 5, "Awesome!");
        _repository.GetByUserIdAndInventoryId(_guid, 1)
            .Returns(Task.FromResult((Review?)null));
        
        //Act
        var command = new AddReviewCommand(_repository, review);

        var canExecute = await command.CanExecute();
        
        //Assert
        
        Assert.True(canExecute);
    }
    
    [Fact]
    public async void AddReviewCommand_CanExecute_Should_Return_False_When_Review_Found()
    {
        //Arrange
        var review = new Review(_guid, 1, "dranzture", 5, "Awesome!");
        _repository.GetByUserIdAndInventoryId(_guid, 1, new CancellationToken())
            .Returns(Task.FromResult(review));
        
        //Act
        var command = new AddReviewCommand(_repository, review);

        var canExecute = await command.CanExecute();
        
        //Assert
        
        Assert.False(canExecute);
    }
    
    [Fact]
    public async void UpdateReviewCommand_CanExecute_Should_Return_True()
    {
        //Arrange
        var review = new Review(_guid, 1, "dranzture", 5, "Awesome!");
        var username = "dranzture";
        _repository.GetById(_guid)
            .Returns(Task.FromResult(review));
        
        //Act
        var command = new UpdateReviewCommand(_repository, review, username);

        var canExecute = await command.CanExecute();
        
        //Assert
        
        Assert.True(canExecute);
    }
    
    [Fact]
    public async void UpdateReviewCommand_CanExecute_Should_Return_False_If_Review_Not_Found()
    {
        //Arrange
        var review = new Review(_guid, 1, "dranzture", 5, "Awesome!");
        var username = "dranzture";
        _repository.GetById(_guid)
            .Returns(Task.FromResult((Review?)null));
        
        //Act
        var command = new UpdateReviewCommand(_repository, review, username);

        var canExecute = await command.CanExecute();
        
        //Assert
        
        Assert.False(canExecute);
    }
    
    [Fact]
    public async void UpdateReviewCommand_CanExecute_Should_Return_False_If_User_Does_Not_Match()
    {
        //Arrange
        var review = new Review(_guid, 1, "dranzture", 5, "Awesome!");
        var username = "dranzture1";
        _repository.GetById(_guid)
            .Returns(Task.FromResult(review));
        
        //Act
        var command = new UpdateReviewCommand(_repository, review, username);

        var canExecute = await command.CanExecute();
        
        //Assert
        
        Assert.False(canExecute);
    }
    
    [Fact]
    public async void DeleteReviewCommand_CanExecute_Should_Return_True()
    {
        //Arrange
        var review = new Review(_guid, 1, "dranzture", 5, "Awesome!");
        var username = "dranzture";
        _repository.GetById(_guid)
            .Returns(Task.FromResult(review));
        
        //Act
        var command = new DeleteReviewCommand(_repository, review, username);

        var canExecute = await command.CanExecute();
        
        //Assert
        
        Assert.True(canExecute);
    }
    
    [Fact]
    public async void DeleteReviewCommand_CanExecute_Should_Return_False_If_Review_Not_Found()
    {
        //Arrange
        var review = new Review(_guid, 1, "dranzture", 5, "Awesome!");
        var username = "dranzture";
        _repository.GetById(_guid)
            .Returns(Task.FromResult((Review?)null));
        
        //Act
        var command = new DeleteReviewCommand(_repository, review, username);

        var canExecute = await command.CanExecute();
        
        //Assert
        
        Assert.False(canExecute);
    }
    
    [Fact]
    public async void DeleteReviewCommand_CanExecute_Should_Return_False_If_User_Does_Not_Match()
    {
        //Arrange
        var review = new Review(_guid, 1, "dranzture", 5, "Awesome!");
        var username = "dranzture1";
        _repository.GetById(_guid)
            .Returns(Task.FromResult(review));
        
        //Act
        var command = new DeleteReviewCommand(_repository, review, username);

        var canExecute = await command.CanExecute();
        
        //Assert
        
        Assert.False(canExecute);
    }
}