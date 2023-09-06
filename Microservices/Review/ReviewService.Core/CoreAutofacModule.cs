using Autofac;
using ReviewService.Core.Interfaces;
using ReviewService.Core.Repositories;

namespace ReviewService.Core;

public class CoreAutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ReviewRepository>()
            .As<IReviewRepository>()
            .InstancePerLifetimeScope();
    }
}
    
