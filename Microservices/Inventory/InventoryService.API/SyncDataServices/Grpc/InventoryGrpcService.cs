﻿using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcInventoryService;
using GrpcInventoryServiceBase = GrpcInventoryService.GrpcInventoryService.GrpcInventoryServiceBase;
using InventoryService.Core.Dtos;
using InventoryService.Core.Interfaces;

namespace InventoryService.API.SyncDataServices.Grpc;

public class InventoryGrpcService : GrpcInventoryServiceBase
{
    private readonly IInventoryService _service;
    private readonly IMapper _mapper;

    public InventoryGrpcService(IInventoryService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public override async Task<StringValue> AddInventory(GrpcInventoryWithUsernameDto dto, ServerCallContext context)
    {
        try
        {
            var item = _mapper.Map<InventoryDto>(dto.Dto);
            var result = await _service.AddInventory(item, dto.Username);


            return new StringValue()
            {
                Value = result
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
    
    public override async Task<Empty> UpdateInventory(GrpcInventoryWithUsernameDto dto, ServerCallContext context)
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
    
    public override async Task<Empty> DeleteInventory(GrpcInventoryWithUsernameDto dto, ServerCallContext context)
    {
        try
        {
            var inventoryDto = _mapper.Map<InventoryDto>(dto.Dto);
            await _service.DeleteInventory(inventoryDto, dto.Username);
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
    
    public override async Task<Empty> DecreaseInventory(GrpcInventoryQuantityChangeDto dto, ServerCallContext context)
    {
        try
        {
            var changeInventoryQuantityDto = _mapper.Map<ChangeInventoryQuantityDto>(dto);
            await _service.DecreaseInventory(changeInventoryQuantityDto);
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
    
    public override async Task<Empty> IncreaseInventory(GrpcInventoryQuantityChangeDto dto, ServerCallContext context)
    {
        try
        {
            var changeInventoryQuantityDto = _mapper.Map<ChangeInventoryQuantityDto>(dto);
            await _service.IncreaseInventory(changeInventoryQuantityDto);
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
    
    public override async Task<GrpcInventoryDto> GetById(StringValue id, ServerCallContext context)
    {
        try
        {
            var guid = new Guid(id.Value);
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
    
    public override async Task<GrpcInventoryDto> GetByName(StringValue name, ServerCallContext context)
    {
        try
        {
            var returnItem = await _service.GetByName(name.Value);
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
    public override async Task<GrpcListedInventories> GetInventoryList(Empty request, ServerCallContext context)
    {
        try
        {
            var inventories = await _service.GetAllInventory();
            if (inventories == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Inventory Not Found"));
            }

            var returnItems = new GrpcListedInventories();
            foreach (var inventory in inventories)
            {
                returnItems.DtoList.Add(_mapper.Map<GrpcListInventoryDto>(inventory));
            }
            return returnItems;
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