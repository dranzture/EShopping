using ShippingService.Core.Dto;
using ShippingService.Core.Interfaces;

namespace ShippingService.Core.Commands;

public class UpdateShippingStatusCommand : ICommand
{
    private readonly IShippingItemRepository _repository;
    private readonly ShippingItemDto _dto;

    public UpdateShippingStatusCommand(IShippingItemRepository repository, ShippingItemDto dto)
    {
        _repository = repository;
        _dto = dto;
    }
    public Task<bool> CanExecute()
    {
        throw new NotImplementedException();
    }

    public Task Execute()
    {
        throw new NotImplementedException();
    }
}