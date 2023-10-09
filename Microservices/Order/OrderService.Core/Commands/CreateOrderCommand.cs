using OrderService.Core.Dtos;
using OrderService.Core.Entities;
using OrderService.Core.Interfaces;

namespace OrderService.Core.Commands;

public class CreateOrderCommand : ICommand
{
    private readonly IOrderRepository _orderRepository;
    private readonly OrderDto _orderDto;

    public CreateOrderCommand(IOrderRepository orderRepository, OrderDto orderDto)
    {
        _orderRepository = orderRepository;
        _orderDto = orderDto;
    }

    public async Task<bool> CanExecute()
    {
        var result = await _orderRepository.GetByShoppingCartId(_orderDto.ShoppingCartId);
        return result == null;
    }

    public async Task Execute()
    {
        var order = new Order(_orderDto.ShoppingCartId, _orderDto.Status, _orderDto.Username);
        await _orderRepository.AddAsync(order);
        await _orderRepository.SaveChangesAsync();
    }
}