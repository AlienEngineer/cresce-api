using System.Net;
using System.Threading.Tasks;
using Cresce.Api.Models;
using NUnit.Framework;

namespace Cresce.Api.Tests.Controllers
{
    public class ServicesControllerTests : WebApiTests
    {
        [Test]
        public async Task Getting_services_returns_the_list_of_services()
        {
            var client = await GetAuthenticatedEmployeeClient();

            var response = await client.GetAsync($"api/v1/services");

            await ResponseAssert.ListAreEquals(
                new[]
                {
                    new ServiceModel
                    {
                        Id = 1,
                        Name = "Development",
                        Value = 30,
                        Image = GetSampleImage().ToBase64()
                    }
                },
                response
            );
        }

        [Test]
        public async Task Getting_services_without_token_returns_401()
        {
            var client = GetClient();

            var response = await client.GetAsync($"api/v1/services");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task Getting_services_with_expired_token_returns_401()
        {
            var client = GetExpiredClient();

            var response = await client.GetAsync($"api/v1/services");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task Getting_services_without_employee_token_returns_401()
        {
            var client = await GetAuthenticatedClient();

            var response = await client.GetAsync($"api/v1/services");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

    }
}
