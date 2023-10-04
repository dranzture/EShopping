using OrderService.Core.Dtos;
using OrderService.Core.Interfaces;

namespace OrderService.Core.Commands;

public class UpdateOrderStatusCommand : ICommand
{
    private readonly IOrderRepository _orderRepository;
    private readonly UpdateOrderStatusDto _updateOrderStatusDto;

    public UpdateOrderStatusCommand(IOrderRepository orderRepository, UpdateOrderStatusDto updateOrderStatusDto)
    {
        _orderRepository = orderRepository;
        _updateOrderStatusDto = updateOrderStatusDto;
    }
    public async Task<bool> CanExecute()
    {
        var result = await _orderRepository.GetById(_updateOrderStatusDto.Id);
        return result != null;
    }

    public async Task Execute()
    {
        var result = await _orderRepository.GetById(_updateOrderStatusDto.Id);
        result!.UpdateOrderStatus(_updateOrderStatusDto.OrderStatus);
        await _orderRepository.SaveChangesAsync();
    }
}