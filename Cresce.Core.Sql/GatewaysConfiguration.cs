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
            RegisterOrganizationGateways(serviceCollection);
            RegisterEmployeeGateways(serviceCollection);

            RegisterReadOperations<UserDto, User>(serviceCollection);
            RegisterReadOperations<ServiceDto, Service>(serviceCollection);
            RegisterReadOperations<CustomerDto, Customer>(serviceCollection);
            RegisterReadOperations<AppointmentDto, Appointment>(serviceCollection);
            RegisterCreateOperations<AppointmentDto, Appointment>(serviceCollection);
        }

        public static void RegisterDbContext(IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<CresceContext>(builder => { builder.UseSqlServer(connectionString); });
        }

        private static void RegisterEmployeeGateways(IServiceCollection serviceCollection)
        {
            RegisterReadOperations<EmployeeDto, Employee>(serviceCollection);
            serviceCollection.AddTransient<IGetEmployeesGateway, GetEmployeesGateway>();
        }

        private static void RegisterOrganizationGateways(IServiceCollection serviceCollection)
        {
            RegisterReadOperations<OrganizationDto, Organization>(serviceCollection);
            serviceCollection.AddTransient<IGetUserOrganizationsGateway, GetUserOrganizationsGateway>();
        }

        private static void RegisterCreateOperations<TEntityDto, TEntity>(IServiceCollection serviceCollection)
            where TEntityDto : class, IUnwrap<TEntity>, IWrap<TEntity>, IHaveAutoIdentity, new()
        {
            serviceCollection.AddTransient<ICreateEntityGateway<TEntity>, CreateEntityGateway<TEntityDto, TEntity>>();
        }

        private static void RegisterReadOperations<TEntityDto, TEntity>(IServiceCollection serviceCollection)
            where TEntityDto : class, IUnwrap<TEntity>, IWrap<TEntity>, new()
        {
            serviceCollection
                .AddTransient<IGetEntityByIdGateway<TEntity>, GetEntityByIdGateway<TEntityDto, TEntity>>();
            serviceCollection.AddTransient<IGetEntitiesGateway<TEntity>, GetEntitiesGateway<TEntityDto, TEntity>>();
            serviceCollection
                .AddTransient<IGetEntitiesQuery<TEntityDto, TEntity>, GetEntitiesQuery<TEntityDto, TEntity>>();
        }
    }
}