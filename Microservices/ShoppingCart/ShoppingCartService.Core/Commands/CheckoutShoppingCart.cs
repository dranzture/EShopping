using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.Models;
using ShoppingCartService.Core.ValueObjects;

namespace ShoppingCartService.Core.Commands;

public class CheckoutShoppingCart : ICommand
{
    private readonly IShoppingCartRepository _repository;
    private readonly Guid _shoppingCartId;
    private readonly IPublisher _publisher;

    public CheckoutShoppingCart(IShoppingCartRepository repository, Guid shoppingCartId, IPublisher publisher)
    {
        _repository = repository;
        _shoppingCartId = shoppingCartId;
        _publisher = publisher;
    }

    public async Task<bool> CanExecute()
    {
        var result = await _repository.GetShoppingCartById(_shoppingCartId);
        if (result is not { Status: CheckoutStatus.None }) return false;
        return result.ShoppingItems.Count > 0;
    }

    public async Task Execute()
    {
        var cartItem = await _repository.GetShoppingCartById(_shoppingCartId);
        await _publisher.ProcessMessage(IPublisher.CheckoutTopic, new Guid().ToString(), cartItem!);
        cartItem!.UpdateCheckoutStatus(CheckoutStatus.InProgress);
        await _repository.UpdateAsync(cartItem);
        await _repository.SaveChangesAsync();
    }
}