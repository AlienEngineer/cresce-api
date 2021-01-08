using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Cresce.Core;
using Cresce.Core.Authentication;
using Cresce.Core.InMemory;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Cresce.Api.Tests
{
    public abstract class WebApiTests
    {
        private WebApplicationFactory<Startup> _factory = null!;

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

        protected Image GetSampleImage()
        {
            return GatewaysConfiguration.GetSampleImage();
        }

        protected HttpClient GetClient()
        {
            return _factory.CreateClient();
        }

        protected HttpClient GetExpiredClient()
        {
            var client = GetClient();

            var token = MakeTokenFactory()!.MakeInvalidToken();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

            return client;
        }

        protected ITokenFactory MakeTokenFactory()
        {
            return _factory.Services.GetService<ITokenFactory>()!;
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
