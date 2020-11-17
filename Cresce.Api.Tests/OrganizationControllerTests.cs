using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cresce.Core.Organizations;
using NUnit.Framework;

namespace Cresce.Api.Tests
{
    public class OrganizationControllerTests : WebApiTests
    {
        [Test]
        public async Task Getting_organization_returns_organization_dto()
        {
            // Arrange
            var client = GetClient();

            // Act
            var response = await client.GetAsync($"api/v1/myUser/organization");

            // Assert
            response.EnsureSuccessStatusCode();
            var organizations = await response.Content.ReadAsAsync<IEnumerable<Organization>>();
            Assert.That(organizations, Is.Not.Null);
            Assert.That(organizations.Any(), Is.True);
        }

        [Test]
        public async Task Getting_organization_for_non_existing_user_returns_not_found()
        {
            // Arrange
            var client = GetClient();

            // Act
            var response = await client.GetAsync($"api/v1/unknown_user/organization");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
