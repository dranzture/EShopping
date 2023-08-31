using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;

namespace InventoryService.Core.Commands.ReviewCommands;

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
        var review = await _repository.GetByUserIdAndInventoryId(_item.InventoryId, _item.ExternalUserId);
        return review == null;
    }
    public async Task Execute()
    {
        await _repository.Create(_item);
        await _repository.SaveChanges();
    }
}