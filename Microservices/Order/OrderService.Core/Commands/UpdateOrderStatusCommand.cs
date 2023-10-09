using OrderService.Core.Dtos;
using OrderService.Core.Interfaces;

namespace OrderService.Core.Commands;

public class UpdateOrderStatusCommand : ICommand
{
    private readonly IOrderRepository _orderRepository;
    private readonly OrderDto _orderDto;

    public UpdateOrderStatusCommand(IOrderRepository orderRepository, OrderDto orderDto)
    {
        _orderRepository = orderRepository;
        _orderDto = orderDto;
    }
    public async Task<bool> CanExecute()
    {
        var result = await _orderRepository.GetByShoppingCartId(_orderDto.ShoppingCartId);
        return result != null;
    }

    public async Task Execute()
    {
        var result = await _orderRepository.GetByShoppingCartId(_orderDto.ShoppingCartId);
        result!.UpdateOrderStatus(_orderDto.Status);
        await _orderRepository.UpdateAsync(result);
        await _orderRepository.SaveChangesAsync();
    }
}