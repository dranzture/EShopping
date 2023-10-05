using AutoMapper;
using Grpc.Core;
using ShippingService.Core.Commands;
using ShippingService.Core.Dto;
using ShippingService.Core.Interfaces;

namespace ShippingService.Core.Services;

public class ShippingItemService : IShippingItemService
{
    private readonly IShippingItemRepository _repository;
    private readonly IMapper _mapper;

    public ShippingItemService(IShippingItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task UpdateStatus(ShippingItemDto dto, CancellationToken token = default)
    {
        var updateShippingStatusCommand = new UpdateShippingStatusCommand(_repository, dto);
        if (!await updateShippingStatusCommand.CanExecute())
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Reprocess order is not allowed"));
        }

        await updateShippingStatusCommand.Execute();
    }

    public async Task<HashSet<ShippingItemDto>> GetAllShippingItems(CancellationToken token = default)
    {
        var result = await _repository.GetAllShippingItems(token);
        return _mapper.Map<HashSet<ShippingItemDto>>(result);
    }
    

    public async Task<ShippingItemDto?> GetShippingItemById(Guid id, CancellationToken token = default)
    {
        var result = await _repository.GetById(id,token);
        return _mapper.Map<ShippingItemDto>(result);
    }
}