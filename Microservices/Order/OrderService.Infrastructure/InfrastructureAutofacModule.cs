using System.Reflection;
using Autofac;
using MediatR;
using OrderService.Core.Dtos;
using OrderService.Core.Handlers;
using OrderService.Core.Interfaces;
using OrderService.Core.Notifications;
using OrderService.Infrastructure.Helpers;
using OrderService.Infrastructure.Interfaces;
using OrderService.Infrastructure.Publishers;
using Module = Autofac.Module;

namespace OrderService.Infrastructure;

public class InfrastructureAutofacModule : Module
{
    private readonly List<Assembly> _assemblies = new List<Assembly>();

    public InfrastructureAutofacModule(Assembly? callingAssembly = null)
    {
        AddToAssembliesIfNotNull(callingAssembly);
    }

    private void LoadAssemblies()
    {
        // TODO: Replace these types with any type in the appropriate assembly/project
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
        RegisterPublisher(builder);
        RegisterMediatr(builder);
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
        
        builder.RegisterType<CreateOrderNotificationHandler>()
            .As(typeof(INotificationHandler<CreateOrderNotification>))
            .InstancePerLifetimeScope();
        
        builder.RegisterType<UpdateOrderStatusNotificationHandler>()
            .As(typeof(INotificationHandler<UpdateOrderStatusNotification>))
            .InstancePerLifetimeScope();
        
    }

    private void RegisterPublisher(ContainerBuilder builder)
    {
        builder.RegisterType<CreateShippingPublisher>()
            .As(typeof(IMessagePublisher<OrderDto>))
            .InstancePerLifetimeScope();

        builder.RegisterType<ReprocessOrderPublisher>()
            .As(typeof(IMessagePublisher<ReprocessOrderDto>))
            .InstancePerLifetimeScope();
    }
}