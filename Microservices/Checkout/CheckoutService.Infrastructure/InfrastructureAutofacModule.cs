using System.Reflection;
using Autofac;
using CheckoutService.Core.Dtos;
using CheckoutService.Core.Handlers;
using CheckoutService.Core.Interfaces;
using CheckoutService.Core.Requests;
using CheckoutService.Infrastructure.Publisher;
using MediatR;
using Module = Autofac.Module;

namespace CheckoutService.Infrastructure;

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
        
        builder.RegisterType<CreateOrderRequestHandler>()
            .As(typeof(IRequestHandler<CreateOrderRequest>))
            .InstancePerLifetimeScope();
        
        builder.RegisterType<ShoppingCartCheckoutRequestHandler>()
            .As(typeof(IRequestHandler<ShoppingCartCheckoutRequest>))
            .InstancePerLifetimeScope();
    }

    private void RegisterPublisher(ContainerBuilder builder)
    {
        builder.RegisterType<OrderPublisher>()
            .As(typeof(IMessageBusPublisher<OrderDto>))
            .InstancePerLifetimeScope();
        
        builder.RegisterType<ShoppingCartPublisher>()
            .As(typeof(IMessageBusPublisher<ShoppingCartDto>))
            .InstancePerLifetimeScope();

    }
}