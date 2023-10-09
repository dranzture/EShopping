using System.Reflection;
using Autofac;
using MediatR;
using ShippingService.Core.Dto;
using ShippingService.Core.Handlers;
using ShippingService.Core.Interfaces;
using ShippingService.Core.Notifications;
using ShippingService.Infrastructure.Helpers;
using ShippingService.Infrastructure.Interfaces;
using ShippingService.Infrastructure.Mailing;
using Module = Autofac.Module;

namespace ShippingService.Infrastructure;

public class InfrastructureAutofacModule : Module
{
    private readonly List<Assembly> _assemblies = new List<Assembly>();

    public InfrastructureAutofacModule(Assembly? callingAssembly = null)
    {
        AddToAssembliesIfNotNull(callingAssembly);
    }

    private void LoadAssemblies()
    {
        var infrastructureAssembly = Assembly.GetAssembly(typeof(InfrastructureAutofacModule));

        AddToAssembliesIfNotNull(infrastructureAssembly);
    }

    private void AddToAssembliesIfNotNull(Assembly? assembly)
    {
        if (assembly != null)
        {
            _assemblies.Add(assembly);
        }
    }

    protected override void Load(ContainerBuilder builder)
    {
        LoadAssemblies();

        RegisterEf(builder);
        RegisterMediatr(builder);
        RegisterMailing(builder);
    }

    private void RegisterEf(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(EfRepository<>))
            .As(typeof(IRepository<>))
            .InstancePerLifetimeScope();
    }

    private void RegisterMediatr(ContainerBuilder builder)
    {
        builder
            .RegisterType<Mediator>()
            .As<IMediator>()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<DomainEventDispatcher>()
            .As<IDomainEventDispatcher>()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<CreateShippingHandler>()
            .As(typeof(INotificationHandler<CreateShippingNotification>))
            .InstancePerLifetimeScope();
        
        builder.RegisterType<UpdateShippingStatusNotificationHandler>()
            .As(typeof(INotificationHandler<UpdateShippingStatusNotification>))
            .InstancePerLifetimeScope();
    }

    private void RegisterMailing(ContainerBuilder builder)
    {
        builder
            .RegisterType<FakeEmailSender>()
            .As<IEmailSender>()
            .InstancePerLifetimeScope();
    }
}

