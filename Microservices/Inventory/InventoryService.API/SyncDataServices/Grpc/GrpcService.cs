using AutoMapper;
using Grpc.Core;
using GrpcInventoryService;
using GrpcInventoryServiceBase = GrpcInventoryService.GrpcInventoryService.GrpcInventoryServiceBase;
using InventoryService.Core.Dtos;
using InventoryService.Core.Interfaces;

namespace InventoryService.API.SyncDataServices.Grpc;

public class GrpcService : GrpcInventoryServiceBase
{
    private readonly IInventoryService _service;
    private readonly IMapper _mapper;

    public GrpcService(IInventoryService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public override async Task<GrpcIdParam> AddInventory(GrpcMutateInventoryDto dto, ServerCallContext context)
    {
        try
        {
            var item = _mapper.Map<InventoryDto>(dto.Dto);
            var result = await _service.AddInventory(item, dto.Username);


            return new GrpcIdParam()
            {
                Id = result
            };


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
    
    public override async Task<Empty> UpdateInventory(GrpcMutateInventoryDto dto, ServerCallContext context)
    {
        try
        {
            var item = _mapper.Map<InventoryDto>(dto.Dto);
            await _service.UpdateInventory(item, dto.Username);
            return new Empty();
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
    
    public override async Task<Empty> DeleteInventory(GrpcIdParam dto, ServerCallContext context)
    {
        try
        {
            await _service.DeleteInventory(new Guid(dto.Id));
            return new Empty();
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
    
    public override async Task<Empty> DecreaseInventory(GrpcInventoryChangeDto dto, ServerCallContext context)
    {
        try
        {
            await _service.DecreaseInventory(new Guid(dto.Id), dto.Amount, dto.Username);
            return new Empty();
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
    
    public override async Task<Empty> IncreaseInventory(GrpcInventoryChangeDto dto, ServerCallContext context)
    {
        try
        {
            await _service.IncreaseInventory(new Guid(dto.Id), dto.Amount, dto.Username);
            return new Empty();
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
    
    public override async Task<GrpcInventoryDto> GetById(GrpcIdParam dto, ServerCallContext context)
    {
        try
        {
            var guid = new Guid(dto.Id);
            var returnItem = await _service.GetById(guid);
            if (returnItem == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Inventory Not Found"));
            }
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
    
    public override async Task<GrpcInventoryDto> GetByName(GrpcNameParam dto, ServerCallContext context)
    {
        try
        {
            var returnItem = await _service.GetByName(dto.Name);
            if (returnItem == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Inventory Not Found"));
            }
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