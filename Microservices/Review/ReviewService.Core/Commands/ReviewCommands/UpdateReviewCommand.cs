using ReviewService.Core.Interfaces;
using ReviewService.Core.Models;

namespace ReviewService.Core.Commands.ReviewCommands;

public class UpdateReviewCommand : ICommand
{
    private readonly IReviewRepository _repository;
    private readonly Review _item;

    public UpdateReviewCommand(IReviewRepository repository, Review item)
    {
        _repository = repository;
        _item = item;
    }

    public async Task<bool> CanExecute()
    {
        var review = await _repository.GetById(_item.Id);
        return review != null;
    }
    public async Task Execute()
    {
        var review = await _repository.GetById(_item.Id);
        
        if (!string.IsNullOrEmpty(_item.Comment))
            review!.UpdateComment(_item.Comment, _item.Username);
        
        review!.UpdateStars(_item.Stars, _item.Username);
        
        await _repository.UpdateAsync(review);
        await _repository.SaveChanges();
    }
}