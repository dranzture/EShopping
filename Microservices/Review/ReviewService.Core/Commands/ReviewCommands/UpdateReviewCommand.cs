using ReviewService.Core.Interfaces;
using ReviewService.Core.Models;

namespace ReviewService.Core.Commands.ReviewCommands;

public class UpdateReviewCommand : ICommand
{
    private readonly IReviewRepository _repository;
    private readonly Review _item;
    private readonly string _username;

    public UpdateReviewCommand(IReviewRepository repository, Review item, string username)
    {
        _repository = repository;
        _item = item;
        _username = username;
    }

    public async Task<bool> CanExecute()
    {
        var review = await _repository.GetById(_item.Id);
        return review != null && review.CreatedBy == _username;
    }
    public async Task Execute()
    {
        var review = await _repository.GetById(_item.Id);
        
        if (!string.IsNullOrEmpty(_item.Comment))
            review!.UpdateComment(_item.Comment, _username);
        
        review!.UpdateStars(_item.Stars, _username);
        
        await _repository.Update(review);
        await _repository.SaveChanges();
    }
}