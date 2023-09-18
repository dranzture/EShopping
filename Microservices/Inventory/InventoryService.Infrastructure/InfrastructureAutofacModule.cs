using System.Reflection;
using Autofac;
using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;
using InventoryService.Infrastructure.Publishers;
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

    }
    private void RegisterEf(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(EfRepository<>))
            .As(typeof(IRepository<>))
            .InstancePerLifetimeScope();
        
        builder.RegisterType<CheckoutPublisher>()
            .As<ICheckoutPublisher<ShoppingCart>>()
            .InstancePerLifetimeScope();
    }
}