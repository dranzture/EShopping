using MediatR;
using ShoppingCartService.Core.Dtos;

namespace ShoppingCartService.Core.Requests;

public class ReprocessOrderRequest: IRequest
{
    public OrderDto _dto { get; }

    public ReprocessOrderRequest(OrderDto dto)
    {
        _dto = dto;
    }
}