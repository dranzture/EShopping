using AutoMapper;
using Grpc.Core;
using InventoryService.Core.Dtos;
using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;

namespace InventoryService.API.SyncDataServices.Grpc;

public class GrpcService : GrpcInventoryService.GrpcInventoryServiceBase
{
    private readonly IInventoryService _service;
    private readonly IMapper _mapper;

    public GrpcService(IInventoryService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task AddInventory(GrpcMutateInventoryDto dto, string username)
    {
        try
        {
            var item = _mapper.Map<InventoryDto>(dto.Dto);
            await _service.AddInventory(item, dto.Username);
        }
        catch (RpcException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, "Internal Server Error"));
        }
    }
    
    public async Task<GrpcInventoryDto> UpdateInventory(GrpcMutateInventoryDto dto, string username)
    {
        try
        {
            var item = _mapper.Map<InventoryDto>(dto.Dto);
            await _service.UpdateInventory(item, dto.Username);
            return dto.Dto;
        }
        catch (RpcException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, "Internal Server Error"));
        }
    }
    
    public async Task DeleteInventory(GrpcInventoryDto dto, string username)
    {
        try
        {
            var item = _mapper.Map<InventoryDto>(dto);
            await _service.DeleteInventory(item);
        }
        catch (RpcException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, "Internal Server Error"));
        }
    }
    
    public async Task DecreaseInventory(GrpcInventoryChangeDto dto, string username)
    {
        try
        {
            var item = _mapper.Map<InventoryDto>(dto.Dto);
            await _service.DecreaseInventory(item, dto.Amount, dto.Username);
        }
        catch (RpcException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, "Internal Server Error"));
        }
    }
    
    public async Task IncreaseInventory(GrpcInventoryChangeDto dto, string username)
    {
        try
        {
            var item = _mapper.Map<InventoryDto>(dto.Dto);
            await _service.IncreaseInventory(item, dto.Amount, dto.Username);
        }
        catch (RpcException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, "Internal Server Error"));
        }
    }
    
    public async Task<GrpcInventoryDto> GetById(GrpcGetByIdParam dto, string username)
    {
        try
        {
            var guid = new Guid(dto.Id);
            var returnItem = await _service.GetById(guid);
            return _mapper.Map<GrpcInventoryDto>(returnItem);
        }
        catch (RpcException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, "Internal Server Error"));
        }
    }
    
    public async Task<GrpcInventoryDto> GetByName(GrpcGetByNameParam dto, string username)
    {
        try
        {
            var returnItem = await _service.GetByName(dto.Name);
            return _mapper.Map<GrpcInventoryDto>(returnItem);
        }
        catch (RpcException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, "Internal Server Error"));
        }
    }
}