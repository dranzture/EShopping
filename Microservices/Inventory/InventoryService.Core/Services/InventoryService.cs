using AutoMapper;
using Grpc.Core;
using InventoryService.Core.Commands.InventoryCommands;
using InventoryService.Core.Dtos;
using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;

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
            var inventory = new Inventory(dto.Name, dto.Description, dto.InStock, dto.Height, dto.Width, dto.Weight,
                username);
            var addCommand = new AddInventoryCommand(_repository, inventory);
            if (!await addCommand.CanExecute())
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    "This inventory already exists with given name."));
            }

            await addCommand.Execute();
            var result = await addCommand.GetResult();
            return result != null ? result.Id.ToString() : "";
        }
        catch (RpcException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not add inventory due to: {ex.Message}");
            return await Task.FromException<string>(ex);
        }
    }

    public async Task UpdateInventory(InventoryDto dto, string username, CancellationToken token = default)
    {
        try
        {
            var inventory = new Inventory(dto.Name, dto.Description, dto.InStock, dto.Height, dto.Width, dto.Weight,
                username, dto.Id);
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
        }
    }

    public async Task DeleteInventory(Guid id, CancellationToken token = default)
    {
        try
        {

            var deleteCommand = new DeleteInventoryCommand(_repository, id);
            if (await deleteCommand.CanExecute() == false)
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
        }
    }

    public async Task IncreaseInventory(Guid id, int amount, string username,
        CancellationToken token = default)
    {
        try
        {
            var increaseInventoryCommand = new IncreaseInventoryCommand(_repository, id, amount, username);
            if (await increaseInventoryCommand.CanExecute() == false)
            {
                var message = amount < 0
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
        }
    }

    public async Task DecreaseInventory(Guid id, int amount, string username,
        CancellationToken token = default)
    {
        try
        {
            var decreaseInventoryCommand = new DecreaseInventoryCommand(_repository, id, amount, username);
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
            return await _repository.GetAllInventory(token) ?? new HashSet<Inventory>();
        }
        catch (RpcException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not get all inventory due to: {ex.Message}");
            return await Task.FromException<HashSet<Inventory>>(exception: ex);
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
            return await Task.FromException<Inventory>(exception: ex);
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
            return await Task.FromException<Inventory>(exception: ex);
        }
    }
}