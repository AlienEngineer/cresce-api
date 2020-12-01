using System.Net.Http;
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
    }
}
