using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Cresce.Core.InMemory;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Cresce.Api.Tests
{
    public abstract class WebApiTests
    {
        private WebApplicationFactory<Startup> _factory;
        private static HttpClient _client;

        [SetUp]
        public void StartFresh()
        {
            _factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(OverrideServices);
            });
        }

        protected virtual void OverrideServices(IServiceCollection services)
        {
            GatewaysConfiguration.RegisterServices(services);
        }

        protected HttpClient GetClient()
        {
            return _client ??= _factory.CreateClient();
        }

        protected async Task<HttpClient> GetAuthenticatedClient()
        {
            var client = GetClient();
            var login = await client.Login();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", login.Token);

            return client;
        }
    }
}
