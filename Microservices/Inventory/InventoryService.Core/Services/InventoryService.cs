using AutoMapper;
using Grpc.Core;
using InventoryService.Core.Commands;
using InventoryService.Core.Dtos;
using InventoryService.Core.Entities;
using InventoryService.Core.Interfaces;

namespace InventoryService.Core.Services;

public class InventoryService : IInventoryService
{
    private readonly IMapper _mapper;
    private readonly IInventoryRepository _repository;

    public InventoryService(IInventoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<string> AddInventory(InventoryDto dto, string username, CancellationToken token = default)
    {
        try
        {
            var inventory = _mapper.Map<Inventory>(dto);
            var addCommand = new AddInventoryCommand(_repository, inventory, username);
            if (!await addCommand.CanExecute())
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    "This inventory already exists with given name."));
            }

            await addCommand.Execute();
            var result = addCommand.GetResult();
            return result != null ? result.Id.ToString() : "";
        }
        catch (RpcException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not add inventory due to: {ex.Message}");
            throw;
        }
    }

    public async Task UpdateInventory(InventoryDto dto, string username, CancellationToken token = default)
    {
        try
        {
            var inventory = _mapper.Map<Inventory>(dto);
            var updateCommand = new UpdateInventoryCommand(_repository, inventory, username);
            if (!await updateCommand.CanExecute())
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    "Requested inventory does not exist."));
            }

            await updateCommand.Execute();
        }
        catch (RpcException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not update inventory due to: {ex.Message}");
            throw;
        }
    }

    public async Task DeleteInventory(InventoryDto dto, string username, CancellationToken token = default)
    {
        try
        {
            var inventory = _mapper.Map<Inventory>(dto);
            var deleteCommand = new DeleteInventoryCommand(_repository, inventory, username);
            if (!await deleteCommand.CanExecute())
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    "Requested inventory does not exist."));
            }

            await deleteCommand.Execute();
        }
        catch (RpcException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not delete inventory due to: {ex.Message}");
            throw;
        }
    }

    public async Task IncreaseInventory(ChangeInventoryQuantityDto dto,
        CancellationToken token = default)
    {
        try
        {
            var increaseInventoryCommand = new IncreaseInventoryCommand(_repository, dto.InventoryId, dto.Quantity);
            if (!await increaseInventoryCommand.CanExecute())
            {
                var message = dto.Quantity < 0
                    ? "Requested incremental amount is less than 0."
                    : "Requested inventory does not exist.";

                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    message));
            }

            await increaseInventoryCommand.Execute();
        }
        catch (RpcException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not increase inventory due to: {ex.Message}");
            throw;
        }
    }

    public async Task DecreaseInventory(ChangeInventoryQuantityDto dto,
        CancellationToken token = default)
    {
        try
        {
            var decreaseInventoryCommand = new DecreaseInventoryCommand(_repository, dto.InventoryId, dto.Quantity);
            if (!await decreaseInventoryCommand.CanExecute())
            {
                var message = "Cannot decrease stock of the inventory";

                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    message));
            }

            await decreaseInventoryCommand.Execute();
        }
        catch (RpcException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not decrease inventory due to: {ex.Message}");
        }
    }

    public async Task<HashSet<Inventory>> GetAllInventory(CancellationToken token = default)
    {
        try
        {
            return await _repository.GetAllInventory(token);
        }
        catch (RpcException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not get all inventory due to: {ex.Message}");
            throw;
        }
    }

    public async Task<Inventory?> GetById(Guid id, CancellationToken token = default)
    {
        try
        {
            return await _repository.GetById(id, token);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not get all inventory due to: {ex.Message}");
            throw;
        }
    }

    public async Task<Inventory?> GetByName(string name, CancellationToken token = default)
    {
        try
        {
            return await _repository.GetByName(name, token);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not get all inventory due to: {ex.Message}");
            throw;
        }
    }
}