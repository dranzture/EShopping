using Microsoft.EntityFrameworkCore;
using OrderService.Core.Dtos;
using OrderService.Core.Entities;
using OrderService.Core.Interfaces;

namespace OrderService.Core.Commands;

public class CreateOrderCommand : ICommand
{
    private readonly IOrderRepository _orderRepository;
    private readonly CreateOrderDto _order;

    public CreateOrderCommand(IOrderRepository orderRepository, CreateOrderDto order)
    {
        _orderRepository = orderRepository;
        _order = order;
    }

    public async Task<bool> CanExecute()
    {
        var queryable = await _orderRepository.Queryable();
        var exists = await queryable
            .Where(x => x.ShoppingCartId == _order.Order.ShoppingCartId)
            .FirstOrDefaultAsync();
        return exists == null;
    }

    public async Task Execute()
    {
        var order = new Order(_order.Order.ShoppingCartId, _order.Order.OrderStatus, _order.Username);
        await _orderRepository.AddAsync(order);
        await _orderRepository.SaveChangesAsync();
    }
}