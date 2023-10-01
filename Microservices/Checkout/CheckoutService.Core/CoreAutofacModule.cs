using Autofac;
using CheckoutService.Core.Interfaces;
using CheckoutService.Core.Repositories;

namespace CheckoutService.Core;

public class CoreAutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<PaymentMethodRepository>()
            .As<IPaymentMethodRepository>()
            .InstancePerLifetimeScope();
    }
}
    
