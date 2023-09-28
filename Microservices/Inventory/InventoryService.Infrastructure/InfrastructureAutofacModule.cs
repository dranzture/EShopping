using System.Reflection;
using Autofac;
using InventoryService.Core.Handlers;
using InventoryService.Core.Interfaces;
using InventoryService.Core.Requests;
using MediatR;
using Module = Autofac.Module;

namespace InventoryService.Infrastructure;

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
        RegisterMediatr(builder);
    }
    private void RegisterMediatr(ContainerBuilder builder)
    {
        builder
            .RegisterType<Mediator>()
            .As<IMediator>()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<DecreaseInventoryQuantityHandler>()
            .As(typeof(IRequestHandler<DecreaseInventoryQuantityRequest>))
            .InstancePerLifetimeScope();
        
        builder.RegisterType<IncreaseInventoryQuantityHandler>()
            .As(typeof(IRequestHandler<IncreaseInventoryQuantityRequest>))
            .InstancePerLifetimeScope();

    }
    private void RegisterEf(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(EfRepository<>))
            .As(typeof(IRepository<>))
            .InstancePerLifetimeScope();
    }
}