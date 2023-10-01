using System.Reflection;
using Autofac;
using MediatR;
using ShoppingCartService.Core.Dtos;
using ShoppingCartService.Core.Handlers;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.Notifications;
using ShoppingCartService.Core.Requests;
using ShoppingCartService.Infrastructure.Helpers;
using ShoppingCartService.Infrastructure.Interfaces;
using ShoppingCartService.Infrastructure.Publisher;
using Module = Autofac.Module;

namespace ShoppingCartService.Infrastructure;

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
        
        builder.RegisterType<ItemAddedToShoppingCartHandler>()
            .As(typeof(INotificationHandler<ItemAddedToShoppingCartEvent>))
            .InstancePerLifetimeScope();
        
        builder.RegisterType<ItemRemovedFromShoppingCartHandler>()
            .As(typeof(INotificationHandler<ItemRemovedFromShoppingCartEvent>))
            .InstancePerLifetimeScope();
        
        builder.RegisterType<ShoppingCartCheckedOutEventHandler>()
            .As(typeof(INotificationHandler<ShoppingCartCheckedOutEvent>))
            .InstancePerLifetimeScope();
        
        builder.RegisterType<UpdateShoppingCartStatusHandler>()
            .As(typeof(IRequestHandler<UpdateShoppingCartRequest>))
            .InstancePerLifetimeScope();
    }

    private void RegisterPublisher(ContainerBuilder builder)
    {
        builder.RegisterType<CheckoutPublisher>()
            .As(typeof(IPublisher<ShoppingCartDto>))
            .InstancePerLifetimeScope();

        builder.RegisterType<ChangeInventoryQuantityPublisher>()
            .As(typeof(IPublisher<ChangeInventoryQuantityDto>))
            .InstancePerLifetimeScope();
    }
}