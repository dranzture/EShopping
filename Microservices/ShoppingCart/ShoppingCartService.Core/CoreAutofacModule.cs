using Autofac;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.Notifications;
using ShoppingCartService.Core.Repositories;

namespace ShoppingCartService.Core;

public class CoreAutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Services.ShoppingCartService>()
            .As<IShoppingCartService>()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<ShoppingCartRepository>()
            .As<IShoppingCartRepository>()
            .InstancePerLifetimeScope();
    }
}
    
