using ReviewService.Core.Commands.ReviewCommands;
using ReviewService.Core.Interfaces;
using ReviewService.Core.Models;
using NSubstitute;

namespace ReviewService.UnitTests;

public class ReviewCommandsTests
{
    private readonly Guid _guid = new();
    private readonly IReviewRepository _repository = Substitute.For<IReviewRepository>();

    [Fact]
    public async void AddReviewCommand_CanExecute_Should_Return_True()
    {
        //Arrange
        var review = new Review(_guid, "dranzture", 5, "Awesome!");
        _repository.GetByInventoryIdAndUsername(_guid, "dranzture")
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
        var review = new Review(_guid, "dranzture", 5, "Awesome!");
        _repository.GetByInventoryIdAndUsername(_guid, "dranzture")
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
        var review = new Review(_guid, "dranzture", 5, "Awesome!");
        _repository.GetById(_guid)
            .Returns(Task.FromResult(review));
        
        //Act
        var command = new UpdateReviewCommand(_repository, review);

        var canExecute = await command.CanExecute();
        
        //Assert
        
        Assert.True(canExecute);
    }
    
    [Fact]
    public async void UpdateReviewCommand_CanExecute_Should_Return_False_If_Review_Not_Found()
    {
        //Arrange
        var review = new Review(_guid, "dranzture", 5, "Awesome!");
        var username = "dranzture";
        _repository.GetById(_guid)
            .Returns(Task.FromResult((Review?)null));
        
        //Act
        var command = new UpdateReviewCommand(_repository, review);

        var canExecute = await command.CanExecute();
        
        //Assert
        
        Assert.False(canExecute);
    }
    
    
    [Fact]
    public async void DeleteReviewCommand_CanExecute_Should_Return_True()
    {
        //Arrange
        var review = new Review(_guid, "dranzture", 5, "Awesome!");
        var username = "dranzture";
        _repository.GetById(_guid)
            .Returns(Task.FromResult(review));
        
        //Act
        var command = new DeleteReviewCommand(_repository, review);

        var canExecute = await command.CanExecute();
        
        //Assert
        
        Assert.True(canExecute);
    }
    
    [Fact]
    public async void DeleteReviewCommand_CanExecute_Should_Return_False_If_Review_Not_Found()
    {
        //Arrange
        var review = new Review(_guid, "dranzture", 5, "Awesome!");
        var username = "dranzture";
        _repository.GetById(_guid)
            .Returns(Task.FromResult((Review?)null));
        
        //Act
        var command = new DeleteReviewCommand(_repository, review);

        var canExecute = await command.CanExecute();
        
        //Assert
        
        Assert.False(canExecute);
    }
}