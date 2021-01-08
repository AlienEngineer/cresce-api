using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Cresce.Core.Sql.Tests
{
    public abstract class SqlTest<T>
    {
        private DbConnection _connection;
        private ServiceProvider _provider;

        [SetUp]
        public void Setup()
        {
            var serviceCollection = new ServiceCollection();
            GatewaysConfiguration.RegisterServices(serviceCollection);

            serviceCollection.AddDbContext<CresceContext>(builder =>
            {
                builder.UseSqlite(CreateInMemoryDatabase());
                _connection = RelationalOptionsExtension.Extract(builder.Options).Connection;
            }, ServiceLifetime.Transient);

            _provider = serviceCollection.BuildServiceProvider();

            Seed();
        }

        private void Seed()
        {
            using var context = GetService<CresceContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var one = new UserModel
            {
                Id = "myUser"
            };

            context.Add(one);

            context.SaveChanges();
        }
        [TearDown]
        public void DisposeConnection()
        {
            _connection.Dispose();
        }

        protected T MakeGateway()
        {
            return GetService<T>();
        }

        private TService GetService<TService>()
        {
            return _provider.GetService<TService>()!;
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

    }

}
