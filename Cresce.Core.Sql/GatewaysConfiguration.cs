using Cresce.Core.Appointments;
using Cresce.Core.Customers;
using Cresce.Core.Employees.GetEmployees;
using Cresce.Core.Organizations;
using Cresce.Core.Services;
using Cresce.Core.Sql.Appointments;
using Cresce.Core.Sql.Customers;
using Cresce.Core.Sql.Employees;
using Cresce.Core.Sql.GetEntities;
using Cresce.Core.Sql.Organizations;
using Cresce.Core.Sql.Services;
using Cresce.Core.Sql.Users;
using Cresce.Core.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Cresce.Core.Sql
{
    public static class GatewaysConfiguration
    {
        public static void RegisterServices(IServiceCollection serviceCollection)
        {
            RegisterUserGateways(serviceCollection);
            RegisterOrganizationGateways(serviceCollection);
            RegisterEmployeeGateways(serviceCollection);
            RegisterServiceGateways(serviceCollection);
            RegisterCustomerGateways(serviceCollection);
            RegisterAppointmentsGateways(serviceCollection);
        }

        private static void RegisterAppointmentsGateways(IServiceCollection serviceCollection)
        {
            RegisterGetEntities<AppointmentModel, Appointment>(serviceCollection);
        }

        public static void RegisterDbContext(IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<CresceContext>(builder =>
            {
                builder.UseSqlServer(connectionString);
            });
        }

        private static void RegisterCustomerGateways(IServiceCollection serviceCollection)
        {
            RegisterGetEntities<CustomerModel, Customer>(serviceCollection);
        }

        private static void RegisterServiceGateways(IServiceCollection serviceCollection)
        {
            RegisterGetEntities<ServiceModel, Service>(serviceCollection);
        }

        private static void RegisterEmployeeGateways(IServiceCollection serviceCollection)
        {
            RegisterGetEntities<EmployeeModel, Employee>(serviceCollection);
            serviceCollection.AddTransient<IGetEmployeesGateway, GetEmployeesGateway>();
        }

        private static void RegisterOrganizationGateways(IServiceCollection serviceCollection)
        {
            RegisterGetEntities<OrganizationModel, Organization>(serviceCollection);
            serviceCollection.AddTransient<IGetUserOrganizationsGateway, GetUserOrganizationsGateway>();
        }

        private static void RegisterUserGateways(IServiceCollection serviceCollection)
        {
            RegisterGetEntities<UserModel, User>(serviceCollection);
            serviceCollection.AddTransient<IGetUserGateway, GetUserGateway>();
        }

        private static void RegisterGetEntities<TEntityModel, TEntity>(IServiceCollection serviceCollection)
            where TEntityModel : class, IUnwrap<TEntity>, new()
        {
            serviceCollection.AddTransient<IGetEntityById<TEntity>, GetEntityById<TEntityModel, TEntity>>();
            serviceCollection.AddTransient<IGetEntitiesGateway<TEntity>, GetEntitiesGateway<TEntityModel, TEntity>>();
            serviceCollection
                .AddTransient<IGetEntitiesQuery<TEntityModel, TEntity>, GetEntitiesQuery<TEntityModel, TEntity>>();
        }
    }
}
