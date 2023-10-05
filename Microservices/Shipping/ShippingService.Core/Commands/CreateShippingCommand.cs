using ShippingService.Core.Dto;
using ShippingService.Core.Entities;
using ShippingService.Core.Enums;
using ShippingService.Core.Interfaces;

namespace ShippingService.Core.Commands;

public class CreateShippingCommand : ICommand
{
    private readonly IShippingItemRepository _repository;
    private readonly OrderDto _dto;

    public CreateShippingCommand(IShippingItemRepository repository, OrderDto dto)
    {
        _repository = repository;
        _dto = dto;
    }
    
    public async Task<bool> CanExecute()
    {
        var shippingItem = await _repository.GetByOrderId(_dto.Id); 
        return shippingItem == null;
    }

    public async Task Execute()
    {
        var shippingItem = new ShippingItem(_dto.Id, ShippingStatus.LabelCreated, _dto.Username);
        await _repository.AddAsync(shippingItem);
        await _repository.SaveChangesAsync();
    }
}