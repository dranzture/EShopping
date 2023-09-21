using InventoryService.Core.Commands.ShoppingCartCommands;
using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;
using InventoryService.Core.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InventoryService.Core.Handlers;

public class UpdateShoppingCartStatusHandler : IRequestHandler<UpdateShoppingCartRequest>
{
    private readonly IShoppingCartRepository _repository;
    private readonly ILogger<UpdateShoppingCartStatusHandler> _logger;

    public UpdateShoppingCartStatusHandler(IShoppingCartRepository repository, ILogger<UpdateShoppingCartStatusHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Handle(UpdateShoppingCartRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var shoppingCart = request._cart;
            var command = new UpdateCheckoutStatusCommand(_repository, shoppingCart.Id, shoppingCart.Status);
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