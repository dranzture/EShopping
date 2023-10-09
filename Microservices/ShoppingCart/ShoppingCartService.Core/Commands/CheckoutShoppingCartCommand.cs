using ShoppingCartService.Core.Dtos;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.Notifications;
using ShoppingCartService.Core.ValueObjects;

namespace ShoppingCartService.Core.Commands;

public class CheckoutShoppingCartCommand : ICommand
{
    private readonly IShoppingCartRepository _repository;
    private readonly Guid _shoppingCartId;
    private readonly IPublisher<ShoppingCartDto> _publisher;

    public CheckoutShoppingCartCommand(IShoppingCartRepository repository, Guid shoppingCartId,
        IPublisher<ShoppingCartDto> publisher)
    {
        _repository = repository;
        _shoppingCartId = shoppingCartId;
        _publisher = publisher;
    }

    public async Task<bool> CanExecute()
    {
        var result = await _repository.GetShoppingCartById(_shoppingCartId);
        return result != null;
    }

    public async Task Execute()
    {
        var cartItem = await _repository.GetShoppingCartById(_shoppingCartId);
        var shoppingItems =
            cartItem!.ShoppingItems.Select(item => new ShoppingItemDto()
            {
                InventoryId = item.InventoryId, Quantity = item.Quantity, ShoppingCartId = _shoppingCartId,
                TotalPrice = item.TotalPrice
            }).ToList();
        await _publisher.ProcessMessage(IPublisher<ShoppingCartDto>.CheckoutTopic, new Guid().ToString(),
            new ShoppingCartDto()
            {
                Id = cartItem.Id,
                Username = cartItem.Username,
                ShoppingItems = shoppingItems
            });
        cartItem!.UpdateCheckoutStatus(CheckoutStatus.InProgress);
        cartItem.AddDomainEvent(new ShoppingCartCheckedOutEvent(cartItem.Username));
        await _repository.UpdateAsync(cartItem);
        await _repository.SaveChangesAsync();
    }
}