using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.Interfaces;

namespace ShoppingCartService.Core.Commands;

public class AddShoppingCart : ICommand
{
    private ShoppingCart? _result = null;
    private readonly IShoppingCartRepository _repository;
    private readonly ShoppingCart _item;

    public AddShoppingCart(IShoppingCartRepository repository, ShoppingCart item)
    {
        _repository = repository;
        _item = item;
    }
    public async Task<bool> CanExecute()
    {
        var item = await _repository.GetShoppingCartByUsername(_item.Username);
        return item == null;
    }

    public async Task Execute()
    {
        await _repository.AddAsync(_item);
        await _repository.SaveChangesAsync();
        _result = _item;
    }

    public ShoppingCart? GetResult()
    {
        return _result ?? null;
    }
}