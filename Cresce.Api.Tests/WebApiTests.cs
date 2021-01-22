using System.Data.Common;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Cresce.Core;
using Cresce.Core.Authentication;
using Cresce.Core.Sql;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Cresce.Api.Tests
{
    public abstract class WebApiTests
    {
        private WebApplicationFactory<Startup> _factory = null!;
        private DbConnection _connection = null!;

        [SetUp]
        public void StartFresh()
        {
            _factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(OverrideServices);
            });
        }

        private void OverrideServices(IServiceCollection services)
        {
            services.AddScoped(_ =>
            {
                _connection = CreateInMemoryDatabase();
                var options = new DbContextOptionsBuilder<CresceContext>()
                    .UseSqlite(_connection)
                    .Options;

                var context = new CresceContext(options);
                context.DeleteDatabase();
                context.Seed();

                return options;
            });
            services.AddDbContext<CresceContext>();
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        public Image GetSampleImage()
        {
            return new(
                "/9j/4AAQSkZJRgABAQEASABIAAD/4gIcSUND/Ko2JJuhuCempcX2PS6FS+fgcegih7FjXQ+tbTWulH0f6W/IlbGzaxVTo1L1SL1FIcVyp+R8N+HMz//Z");
        }

        protected HttpClient GetClient()
        {
            return _factory.CreateClient();
        }

        protected HttpClient GetExpiredClient()
        {
            var client = GetClient();

            var token = MakeTokenFactory()!.MakeExpiredAuthorization();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

            return client;
        }

        protected IAuthorizationFactory MakeTokenFactory()
        {
            var scope = _factory.Services.CreateScope();
            return scope.ServiceProvider.GetService<IAuthorizationFactory>()!;
        }

        protected async Task<HttpClient> GetAuthenticatedClient()
        {
            var client = GetClient();
            var login = await client.Login();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", login.Token);

            return client;
        }

        protected async Task<HttpClient> GetAuthenticatedEmployeeClient()
        {
            var client = GetClient();
            var login = await client.Login();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", login.Token);

            var employeeLogin = await client.LoginEmployee();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", employeeLogin.Token);

            return client;
        }
    }
}