using System.Data.Common;
using Cresce.Core.Authentication;
using Cresce.Core.Sql;
using Cresce.Core.Users;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Cresce.Core.Tests
{
    public class ServicesTests<T>
    {
        private ServiceProvider _provider = null!;
        private DbConnection _connection = null!;

        [SetUp]
        public void Setup()
        {
            var serviceCollection = new ServiceCollection();
            GatewaysConfiguration.RegisterServices(serviceCollection);
            ServicesConfiguration.RegisterServices(serviceCollection);

            serviceCollection.AddDbContext<CresceContext>(builder =>
            {
                builder.UseSqlite(CreateInMemoryDatabase());
                _connection = RelationalOptionsExtension.Extract(builder.Options).Connection;
            }, ServiceLifetime.Transient);

            _provider = serviceCollection.BuildServiceProvider();

            GetService<CresceContext>().Seed();
        }

        [TearDown]
        public void DisposeConnection() => _connection.Dispose();

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        protected Image GetSampleImage() => new(GetService<CresceContext>().GetSampleImage());

        protected T MakeService() => GetService<T>();

        protected IAuthorization GetAuthorization() =>
            GetService<IAuthorizationFactory>().MakeAuthorization(new AdminUser
            {
                Id = "myUser"
            });

        protected IEmployeeAuthorization GetEmployeeAuthorization()
        {
            var factory = GetService<IAuthorizationFactory>();
            return factory.GetAuthorizedEmployee(
                factory.MakeAuthorization(new AdminUser {Id = "myUser"}),
                1
            );
        }

        protected IEmployeeAuthorization GetInvalidEmployeeAuthorization()
        {
            var factory = GetService<IAuthorizationFactory>();
            return factory.GetAuthorizedEmployee(
                factory.MakeAuthorization(new AdminUser {Id = "myUser"}),
                -1
            );
        }

        protected IEmployeeAuthorization GetExpiredEmployeeAuthorization() =>
            GetService<IAuthorizationFactory>().MakeExpiredEmployeeAuthorization();

        protected IAuthorization GetUnknownUser() =>
            GetService<IAuthorizationFactory>().MakeAuthorization(new UnknownUser());

        protected IAuthorization GetExpiredAuthorization() =>
            GetService<IAuthorizationFactory>().MakeExpiredAuthorization();

        private TService GetService<TService>() => _provider.GetService<TService>()!;
    }
}