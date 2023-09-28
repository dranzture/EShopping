using InventoryService.Core.Commands;
using InventoryService.Core.Interfaces;
using InventoryService.Core.Requests;
using MediatR;
using Microsoft.Extensions.Logging;


namespace InventoryService.Core.Handlers;

public class IncreaseInventoryQuantityHandler : IRequestHandler<IncreaseInventoryQuantityRequest>
{
    private readonly IInventoryRepository _repository;
    private readonly ILogger<IncreaseInventoryQuantityHandler> _logger;

    public IncreaseInventoryQuantityHandler(IInventoryRepository repository, ILogger<IncreaseInventoryQuantityHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Handle(IncreaseInventoryQuantityRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var command = new IncreaseInventoryCommand(_repository, request.InventoryId, request.Quantity);
            if (!await command.CanExecute())
            {
                throw new ArgumentException("Cannot update shopping cart's status in handler");
            }
            await command.Execute();
        }
        catch(ArgumentException ex)
        {
            _logger.LogError(ex.Message);
            
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
        }

    }
}