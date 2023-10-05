using Autofac;
using ShippingService.Core.Interfaces;
using ShippingService.Core.Repositories;

namespace ShippingService.Core;

public class CoreAutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Services.ShippingItemService>()
            .As<IShippingItemService>()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<ShippingItemRepository>()
            .As<IShippingItemRepository>()
            .InstancePerLifetimeScope();
    }
}
    
