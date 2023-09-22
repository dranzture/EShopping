using Autofac;
using InventoryService.Core.Repositories;
using ShoppingCartService.Core.Interfaces;

namespace ShoppingCartService.Core;

public class CoreAutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Services.ShoppingCartService>()
            .As<IShoppingCartService>()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<IShoppingCartRepository>()
            .As<ShoppingCartRepository>()
            .InstancePerLifetimeScope();
    }
}
    
