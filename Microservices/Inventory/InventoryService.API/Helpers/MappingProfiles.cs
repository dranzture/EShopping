using AutoMapper;
using InventoryService.Core.Dtos;
using InventoryService.Core.Models;
using GrpcInventoryDto = GrpcInventoryService.GrpcInventoryDto;

namespace InventoryService.API.Helpers;

public class MappingProfiles : Profile
{
    //To avoid violations to Clean Architecture. Grpc models -> Core models -> Persistence models
    public MappingProfiles()
    {
        CreateMap<GrpcInventoryDto, InventoryDto>()
            .ForMember(e => e.Id, t =>
                t.MapFrom(e => new Guid(e.Id)));;
        
        CreateMap<InventoryDto, Inventory>();

        CreateMap<Inventory, GrpcInventoryDto>().ForMember(e => e.Id, t =>
            t.MapFrom(e => e.Id.ToString()));;
        
        CreateMap<GrpcInventoryDto, InventoryDto>()
            .ForMember(dest => dest.Id, opt 
                => opt.MapFrom(src => !string.IsNullOrEmpty(src.Id) ? new Guid(src.Id) : (Guid?)null));
        
        CreateMap<InventoryDto, GrpcInventoryDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.HasValue ? src.Id.Value.ToString() : string.Empty));
        
    }
}