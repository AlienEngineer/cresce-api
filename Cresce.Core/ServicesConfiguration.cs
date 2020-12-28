using Cresce.Core.Authentication;
using Cresce.Core.Employees;
using Cresce.Core.Organizations;
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
            serviceCollection.AddTransient<ILoginService, LoginService>();
            serviceCollection.AddTransient<IOrganizationService, OrganizationService>();
            serviceCollection.AddTransient<IEmployeeService, EmployeeService>();
            serviceCollection.AddTransient<ITokenFactory, TokenFactory>();
        }
    }
}
