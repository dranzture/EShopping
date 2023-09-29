using ReviewService.Core.Interfaces;
using ReviewService.Core.Models;

namespace ReviewService.Core.Commands.ReviewCommands;

public class AddReviewCommand : ICommand
{
    private readonly IReviewRepository _repository;
    private readonly Review _item;

    public AddReviewCommand(IReviewRepository repository, Review item)
    {
        _repository = repository;
        _item = item;
    }
    public async Task<bool> CanExecute()
    {
        var review = await _repository.GetByInventoryIdAndUsername(_item.InventoryId, _item.Username);
        return review == null;
    }
    public async Task Execute()
    {
        await _repository.AddAsync(_item);
        await _repository.SaveChanges();
    }

    public Review? GetResult()
    {
        return _item;
    }
}