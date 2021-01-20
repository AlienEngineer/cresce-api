using Cresce.Core.Services.GetServices;
using Microsoft.Extensions.DependencyInjection;

namespace Cresce.Core.Services
{
    internal class ServicesModule : IServicesModule
    {
        public void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IGetServicesService, GetServicesService>();
            serviceCollection.AddTransient<IServiceServices, ServiceServices>();
        }
    }
}