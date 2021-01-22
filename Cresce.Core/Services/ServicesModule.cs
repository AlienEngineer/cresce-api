using Microsoft.Extensions.DependencyInjection;

namespace Cresce.Core.Services
{
    internal class ServicesModule : IServicesModule
    {
        public void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IServiceServices, ServiceServices>();
            serviceCollection.RegisterGetEntities<Service>();
        }
    }
}