using ReviewService.Core.Interfaces;
using ReviewService.Core.Models;

namespace ReviewService.Core.Commands.ReviewCommands;

public class DeleteReviewCommand : ICommand
{
    private readonly IReviewRepository _repository;
    private readonly Review _item;
    
    public DeleteReviewCommand(IReviewRepository repository, Review item)
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

        await _repository.DeleteAsync(review!);
        await _repository.SaveChanges();
    }
}