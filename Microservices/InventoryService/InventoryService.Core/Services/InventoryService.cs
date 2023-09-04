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

    public async Task AddInventory(InventoryDto dto, string username, CancellationToken token = default)
    {
        try
        {
            var inventory = _mapper.Map<Inventory>(dto);
            var addCommand = new AddInventoryCommand(_repository, inventory);
            if (await addCommand.CanExecute() == false)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    "This inventory already exists with given name."));
            }

            await addCommand.Execute();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not add inventory due to: {ex.Message}");
        }
    }

    public async Task UpdateInventory(InventoryDto dto, string username, CancellationToken token = default)
    {
        try
        {
            var inventory = _mapper.Map<Inventory>(dto);
            var addCommand = new UpdateInventoryCommand(_repository, inventory, username);
            if (await addCommand.CanExecute() == false)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    "Requested inventory does not exist."));
            }

            await addCommand.Execute();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not update inventory due to: {ex.Message}");
        }
    }

    public async Task DeleteInventory(InventoryDto dto, string username, CancellationToken token = default)
    {
        try
        {
            var inventory = _mapper.Map<Inventory>(dto);
            var addCommand = new DeleteInventoryCommand(_repository, inventory);
            if (await addCommand.CanExecute() == false)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    "Requested inventory does not exist."));
            }

            await addCommand.Execute();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not delete inventory due to: {ex.Message}");
        }
    }

    public async Task IncreaseInventory(InventoryDto dto, int amount, string username,
        CancellationToken token = default)
    {
        try
        {
            var inventory = _mapper.Map<Inventory>(dto);
            var addCommand = new IncreaseInventoryCommand(_repository, inventory, amount, username);
            if (await addCommand.CanExecute() == false)
            {
                var message = amount < 0
                    ? "Requested incremental amount is less than 0."
                    : "Requested inventory does not exist.";

                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    message));
            }

            await addCommand.Execute();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not increase inventory due to: {ex.Message}");
        }
    }

    public async Task DecreaseInventory(InventoryDto dto, int amount, string username,
        CancellationToken token = default)
    {
        try
        {
            var inventory = _mapper.Map<Inventory>(dto);
            var addCommand = new IncreaseInventoryCommand(_repository, inventory, amount, username);
            if (await addCommand.CanExecute() == false)
            {
                var message = amount < 0
                    ? "Requested decremental amount is less than 0."
                    : inventory.InStock - amount < 0
                        ? "Requested decremental amount cannot be more than in stock amount."
                        : "Requested inventory does not exist.";

                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    message));
            }

            await addCommand.Execute();
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
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not get all inventory due to: {ex.Message}");
            return await Task.FromException<HashSet<Inventory>>(exception: ex);
        }
    }

    public async Task<Inventory> GetById(Guid id, CancellationToken token = default)
    {
        try
        {
            return await _repository.GetById(id,token);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not get all inventory due to: {ex.Message}");
            return await Task.FromException<Inventory>(exception: ex);
        }
    }

    public async Task<Inventory> GetByName(string name, CancellationToken token = default)
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