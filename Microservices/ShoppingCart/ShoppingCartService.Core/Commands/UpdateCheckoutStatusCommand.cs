using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.Models;
using CheckoutStatus = ShoppingCartService.Core.Entities.ShoppingCart.CheckoutStatus;

namespace ShoppingCartService.Core.Commands;

public class UpdateCheckoutStatusCommand : ICommand
{
    private readonly IShoppingCartRepository _repository;
    private readonly Guid _cartId;
    private readonly CheckoutStatus _status;

    public UpdateCheckoutStatusCommand(IShoppingCartRepository repository, Guid cartId, CheckoutStatus status)
    {
        _repository = repository;
        _cartId = cartId;
        _status = status;
    }

    public async Task<bool> CanExecute()
    {
        var result = await _repository.GetShoppingCartById(_cartId);
        if (result == null) return false;
        return result.Status != ShoppingCart.CheckoutStatus.Completed && _status != ShoppingCart.CheckoutStatus.None;
    }

    public async Task Execute()
    {
        var result = await _repository.GetShoppingCartById(_cartId);
        result!.UpdateCheckoutStatus(_status);
        await _repository.SaveChangesAsync();
    }
}