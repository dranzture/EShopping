﻿using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;

namespace InventoryService.Core.Commands.ReviewCommands;

public class DeleteReviewCommand : ICommand
{
    private readonly IReviewRepository _repository;
    private readonly Review _item;
    private readonly string _username;
    
    public DeleteReviewCommand(IReviewRepository repository, Review item, string username)
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

        await _repository.Delete(review);
        await _repository.SaveChanges();
    }
}