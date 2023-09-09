using Autofac;
using InventoryService.Core.Interfaces;
using InventoryService.Core.Repositories;

namespace InventoryService.Core;

public class CoreAutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<InventoryRepository>()
            .As<IInventoryRepository>()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<Services.InventoryService>()
            .As<IInventoryService>()
            .InstancePerLifetimeScope();
    }
}
    
