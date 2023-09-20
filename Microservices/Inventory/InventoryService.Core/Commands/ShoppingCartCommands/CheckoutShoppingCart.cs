using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;

namespace InventoryService.Core.Commands.ShoppingCartCommands;

public class CheckoutShoppingCart : ICommand
{
    private readonly IShoppingCartRepository _repository;
    private readonly ShoppingCart _cart;
    private readonly IPublisher<string,ShoppingCart> _publisher;

    public CheckoutShoppingCart(IShoppingCartRepository repository, ShoppingCart cart, IPublisher<string,ShoppingCart> publisher)
    {
        _repository = repository;
        _cart = cart;
        _publisher = publisher;
    }

    public async Task<bool> CanExecute()
    {
        var result = await _repository.GetShoppingCartById(_cart.Id);
        if (result is not { Status: ShoppingCart.CheckoutStatus.None }) return false;
        return result.ShoppingItems.Count > 0;
    }

    public async Task Execute()
    {
        var cartItem = await _repository.GetShoppingCartById(_cart.Id);
        await _publisher.ProcessMessage(IPublisher<string, ShoppingCart>.CheckoutTopic, new Guid().ToString(), cartItem!);
    }
}