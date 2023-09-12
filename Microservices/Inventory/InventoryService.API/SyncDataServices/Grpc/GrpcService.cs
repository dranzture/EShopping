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

    public override Task<GrpcIdParam> AddInventory(GrpcMutateInventoryDto dto, ServerCallContext context)
    {
        try
        {
            var item = _mapper.Map<InventoryDto>(dto.Dto);
            var result = _service.AddInventory(item, dto.Username);


            return Task.FromResult(new GrpcIdParam()
            {
                Id = result
            });
            
            
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
    
    public override Task<Empty> UpdateInventory(GrpcMutateInventoryDto dto, ServerCallContext context)
    {
        try
        {
            var item = _mapper.Map<InventoryDto>(dto.Dto);
            _service.UpdateInventory(item, dto.Username);
            return Task.FromResult(new Empty());
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
    
    public override  Task<Empty> DeleteInventory(GrpcIdParam dto, ServerCallContext context)
    {
        try
        {
            _service.DeleteInventory(new Guid(dto.Id));
            return Task.FromResult(new Empty());
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
    
    public override Task<Empty> DecreaseInventory(GrpcInventoryChangeDto dto, ServerCallContext context)
    {
        try
        {
            _service.DecreaseInventory(new Guid(dto.Id), dto.Amount, dto.Username);
            return Task.FromResult(new Empty());
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
    
    public override Task<Empty> IncreaseInventory(GrpcInventoryChangeDto dto, ServerCallContext context)
    {
        try
        {
            _service.IncreaseInventory(new Guid(dto.Id), dto.Amount, dto.Username);
            return Task.FromResult(new Empty());
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
    
    public override Task<GrpcInventoryDto> GetById(GrpcIdParam dto, ServerCallContext context)
    {
        try
        {
            var guid = new Guid(dto.Id);
            var returnItem = _service.GetById(guid);
            if (returnItem == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Inventory Not Found"));
            }
            return Task.FromResult(_mapper.Map<GrpcInventoryDto>(returnItem));
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
    
    public override Task<GrpcInventoryDto> GetByName(GrpcNameParam dto, ServerCallContext context)
    {
        try
        {
            var returnItem =  _service.GetByName(dto.Name);
            if (returnItem == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Inventory Not Found"));
            }
            return Task.FromResult(_mapper.Map<GrpcInventoryDto>(returnItem));
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