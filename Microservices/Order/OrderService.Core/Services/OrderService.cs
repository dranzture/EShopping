using AutoMapper;
using Grpc.Core;
using OrderService.Core.Commands;
using OrderService.Core.Dtos;
using OrderService.Core.Interfaces;

namespace OrderService.Core.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMessagePublisher<ReprocessOrderDto> _publisher;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, 
        IMessagePublisher<ReprocessOrderDto> publisher,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _publisher = publisher;
        _mapper = mapper;
    }

    public async Task ReprocessOrderById(Guid id, CancellationToken token = default)
    {
        var reprocessCommand = new ReprocessOrderCommand(_orderRepository, id, _publisher);
        if (!await reprocessCommand.CanExecute())
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Reprocess order is not allowed"));
        }

        await reprocessCommand.Execute();
    }

    public async Task<HashSet<OrderDto>> GetAllOrders(CancellationToken token = default)
    {
        var result = await _orderRepository.GetAllOrders(token);
        return _mapper.Map<HashSet<OrderDto>>(result);
    }

    public async Task<HashSet<OrderDto>> GetOrdersByUsername(string username, CancellationToken token = default)
    {
        var result = await _orderRepository.GetOrdersByUsername(username,token);
        return _mapper.Map<HashSet<OrderDto>>(result);
    }

    public async Task<OrderDto?> GetOrderById(Guid id, CancellationToken token = default)
    {
        var result = await _orderRepository.GetById(id,token);
        return _mapper.Map<OrderDto>(result);
    }
}