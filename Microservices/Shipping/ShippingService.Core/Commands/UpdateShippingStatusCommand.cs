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
    public async Task<bool> CanExecute()
    {
        var shippingItem = await _repository.GetById(_dto.Id); 
        return shippingItem != null;
    }

    public async Task Execute()
    {
        var shippingItem = await _repository.GetById(_dto.Id); 
        shippingItem!.UpdateStatus(_dto.Status);
        await _repository.UpdateAsync(shippingItem);
        await _repository.SaveChangesAsync();
    }
}