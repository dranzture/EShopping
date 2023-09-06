using AutoMapper;
using InventoryService.Core.Dtos;
using InventoryService.Core.Models;

namespace InventoryService.API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<InventoryDto, Inventory>();
    }
}