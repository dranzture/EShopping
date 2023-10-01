using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.Interfaces;

namespace ShoppingCartService.Core.Commands;

public class AddShoppingCart : ICommand
{
    private ShoppingCart? _result = null;
    private readonly IShoppingCartRepository _repository;
    private readonly string _username;

    public AddShoppingCart(IShoppingCartRepository repository, string username)
    {
        _repository = repository;
        _username = username;
    }
    public async Task<bool> CanExecute()
    {
        var item = await _repository.GetShoppingCartByUsername(_username);
        return item == null;
    }

    public async Task Execute()
    {
        var newShoppingCart = new ShoppingCart(_username);
        await _repository.AddAsync(newShoppingCart);
        await _repository.SaveChangesAsync();
        _result = newShoppingCart;
    }

    public ShoppingCart? GetResult()
    {
        return _result ?? null;
    }
}