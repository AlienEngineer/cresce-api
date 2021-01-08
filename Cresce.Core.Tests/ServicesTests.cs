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
        public void DisposeConnection()
        {
            _connection.Dispose();
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        protected Image GetSampleImage() => new(GetService<CresceContext>().GetSampleImage());

        protected T MakeService() => GetService<T>();

        protected AuthorizedUser GetAuthorizedUser()
        {
            return GetService<ITokenFactory>().MakeToken(new BasicUser
            {
                Id = "myUser"
            });
        }

        protected AuthorizedUser GetUnknownUser()
        {
            return GetService<ITokenFactory>().MakeToken(new UnknownUser());
        }

        protected AuthorizedUser GetExpiredUser()
        {
            return GetService<ITokenFactory>().MakeInvalidToken();
        }

        private TService GetService<TService>()
        {
            return _provider.GetService<TService>()!;
        }
    }
}
