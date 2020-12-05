using System.Net;
using System.Threading.Tasks;
using Cresce.Core.Organizations;
using NUnit.Framework;

namespace Cresce.Api.Tests.Controllers
{
    public class OrganizationsControllerTests : WebApiTests
    {
        [Test]
        public async Task Getting_organization_returns_organization_dto()
        {
            var client = await GetAuthenticatedClient();

            var response = await client.GetAsync("api/v1/organizations");

            await ResponseAssert.ListAreEquals(
                new [] { new Organization { Name = "myOrganization" } },
                response
            );
        }

        [Test]
        public async Task Getting_organization_without_authentication_returns_401()
        {
            var client = GetClient();

            var response = await client.GetAsync("api/v1/organizations");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task Getting_organization_with_expired_token_returns_401()
        {
            var client = GetExpiredClient();

            var response = await client.GetAsync("api/v1/organizations");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
    }
}
