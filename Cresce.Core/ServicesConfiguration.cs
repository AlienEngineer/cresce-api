using Cresce.Core.Appointments;
using Cresce.Core.Authentication;
using Cresce.Core.Customers;
using Cresce.Core.Employees;
using Cresce.Core.GetEntities;
using Cresce.Core.Organizations;
using Cresce.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace System.Runtime.CompilerServices
{
    public class IsExternalInit{}
}

namespace Cresce.Core
{
    public static class ServicesConfiguration
    {
        public static void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.RegisterModule<EmployeesModule>();
            serviceCollection.RegisterModule<ServicesModule>();
            serviceCollection.RegisterModule<CustomersModule>();
            serviceCollection.RegisterModule<AppointmentsModule>();
            serviceCollection.AddTransient<ILoginService, LoginService>();
            serviceCollection.AddTransient<IOrganizationService, OrganizationService>();
            serviceCollection.AddTransient<IAuthorizationFactory, AuthorizationFactory>();
            serviceCollection.AddTransient(provider => new Settings(provider.GetService<IConfiguration>()));
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static void RegisterModule<TModule>(this IServiceCollection serviceCollection) where TModule : IServicesModule, new()
        {
            new TModule().RegisterServices(serviceCollection);
        }

        public static void RegisterGetEntities<TEntity>(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IGetEntitiesService<TEntity>, GetEntitiesService<TEntity>>();
        }
    }

    public interface IServicesModule
    {
        void RegisterServices(IServiceCollection serviceCollection);
    }
}
