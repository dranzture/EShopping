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

    public string AddInventory(InventoryDto dto, string username, CancellationToken token = default)
    {
        try
        {
            var inventory = new Inventory(dto.Name, dto.Description, dto.InStock, dto.Height, dto.Width, dto.Weight,
                username);
            var addCommand = new AddInventoryCommand(_repository, inventory);
            if (!addCommand.CanExecute())
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    "This inventory already exists with given name."));
            }

            addCommand.Execute();
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

    public void UpdateInventory(InventoryDto dto, string username, CancellationToken token = default)
    {
        try
        {
            var inventory = new Inventory(dto.Name, dto.Description, dto.InStock, dto.Height, dto.Width, dto.Weight,
                username, dto.Id);
            var updateCommand = new UpdateInventoryCommand(_repository, inventory, username);
            if (!updateCommand.CanExecute())
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    "Requested inventory does not exist."));
            }

            updateCommand.Execute();
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

    public void DeleteInventory(Guid id, CancellationToken token = default)
    {
        try
        {
            var deleteCommand = new DeleteInventoryCommand(_repository, id);
            if (!deleteCommand.CanExecute())
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    "Requested inventory does not exist."));
            }

            deleteCommand.Execute();
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

    public void IncreaseInventory(Guid id, int amount, string username,
        CancellationToken token = default)
    {
        try
        {
            var increaseInventoryCommand = new IncreaseInventoryCommand(_repository, id, amount, username);
            if (!increaseInventoryCommand.CanExecute())
            {
                var message = amount < 0
                    ? "Requested incremental amount is less than 0."
                    : "Requested inventory does not exist.";

                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    message));
            }

            increaseInventoryCommand.Execute();
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

    public void DecreaseInventory(Guid id, int amount, string username,
        CancellationToken token = default)
    {
        try
        {
            var decreaseInventoryCommand = new DecreaseInventoryCommand(_repository, id, amount, username);
            if (!decreaseInventoryCommand.CanExecute())
            {
                var message = "Cannot decrease stock of the inventory";

                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    message));
            }

            decreaseInventoryCommand.Execute();
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

    public HashSet<Inventory> GetAllInventory(CancellationToken token = default)
    {
        try
        {
            return _repository.GetAllInventory(token);
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

    public Inventory? GetById(Guid id, CancellationToken token = default)
    {
        try
        {
            return _repository.GetById(id, token);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not get all inventory due to: {ex.Message}");
            throw;
        }
    }

    public Inventory? GetByName(string name, CancellationToken token = default)
    {
        try
        {
            return _repository.GetByName(name, token);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not get all inventory due to: {ex.Message}");
            throw;
        }
    }
}