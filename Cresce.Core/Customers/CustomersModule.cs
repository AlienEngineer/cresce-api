using Cresce.Core.Customers.GetCustomers;
using Microsoft.Extensions.DependencyInjection;

namespace Cresce.Core.Customers
{
    internal class CustomersModule : IServicesModule
    {
        public void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IGetCustomersService, GetCustomerService>();
            serviceCollection.AddTransient<ICustomerServices, CustomerServices>();
        }
    }
}
