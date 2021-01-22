using Microsoft.Extensions.DependencyInjection;

namespace Cresce.Core.Customers
{
    internal class CustomersModule : IServicesModule
    {
        public void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ICustomerServices, CustomerServices>();
            serviceCollection.RegisterGetEntities<Customer>();
        }
    }
}