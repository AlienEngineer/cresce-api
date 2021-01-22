using System.Net;
using System.Threading.Tasks;
using Cresce.Api.Models;
using NUnit.Framework;

namespace Cresce.Api.Tests.Controllers
{
    public class CustomersControllerTests : WebApiTests
    {
        [Test]
        public async Task Getting_customers_returns_the_list_of_customers()
        {
            var client = await GetAuthenticatedEmployeeClient();

            var response = await client.GetAsync($"api/v1/customers");

            await ResponseAssert.ListAreEquals(
                new[]
                {
                    new CustomerModel
                    {
                        Id = 1,
                        Name = "Diogo Quintas",
                        Image = GetSampleImage().ToBase64()
                    }
                },
                response
            );
        }

        [Test]
        public async Task Getting_customers_without_token_returns_401()
        {
            var client = GetClient();

            var response = await client.GetAsync($"api/v1/customers");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task Getting_customers_with_expired_token_returns_401()
        {
            var client = GetExpiredClient();

            var response = await client.GetAsync($"api/v1/customers");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task Getting_customers_without_employee_token_returns_401()
        {
            var client = await GetAuthenticatedClient();

            var response = await client.GetAsync($"api/v1/customers");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

    }
}
