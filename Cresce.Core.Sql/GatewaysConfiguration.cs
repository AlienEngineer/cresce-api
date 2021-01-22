using Cresce.Core.Appointments;
using Cresce.Core.Customers;
using Cresce.Core.Employees.GetEmployees;
using Cresce.Core.Organizations;
using Cresce.Core.Services;
using Cresce.Core.Sql.Appointments;
using Cresce.Core.Sql.CreateEntities;
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
            RegisterCrudOperations<UserModel, User>(serviceCollection);
            RegisterOrganizationGateways(serviceCollection);
            RegisterEmployeeGateways(serviceCollection);

            RegisterCrudOperations<ServiceModel, Service>(serviceCollection);
            RegisterCrudOperations<CustomerModel, Customer>(serviceCollection);
            RegisterCrudOperations<AppointmentModel, Appointment>(serviceCollection);
        }

        public static void RegisterDbContext(IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<CresceContext>(builder => { builder.UseSqlServer(connectionString); });
        }

        private static void RegisterEmployeeGateways(IServiceCollection serviceCollection)
        {
            RegisterCrudOperations<EmployeeModel, Employee>(serviceCollection);
            serviceCollection.AddTransient<IGetEmployeesGateway, GetEmployeesGateway>();
        }

        private static void RegisterOrganizationGateways(IServiceCollection serviceCollection)
        {
            RegisterCrudOperations<OrganizationModel, Organization>(serviceCollection);
            serviceCollection.AddTransient<IGetUserOrganizationsGateway, GetUserOrganizationsGateway>();
        }

        private static void RegisterCrudOperations<TEntityModel, TEntity>(IServiceCollection serviceCollection)
            where TEntityModel : class, IUnwrap<TEntity>, IWrap<TEntity>, new()
        {
            serviceCollection
                .AddTransient<IGetEntityByIdGateway<TEntity>, GetEntityByIdGateway<TEntityModel, TEntity>>();
            serviceCollection.AddTransient<IGetEntitiesGateway<TEntity>, GetEntitiesGateway<TEntityModel, TEntity>>();
            serviceCollection
                .AddTransient<IGetEntitiesQuery<TEntityModel, TEntity>, GetEntitiesQuery<TEntityModel, TEntity>>();

            serviceCollection.AddTransient<ICreateEntityGateway<TEntity>, CreateEntityGateway<TEntityModel, TEntity>>();
        }
    }
}