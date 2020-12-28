using System.Net;
using System.Threading.Tasks;
using Cresce.Api.Models;
using NUnit.Framework;

namespace Cresce.Api.Tests.Controllers
{
    public class EmployeesControllerTests : WebApiTests
    {
        [Test]
        public async Task Getting_employees_returns_employees_for_given_organization()
        {
            var client = await GetAuthenticatedClient();

            var response = await client.GetAsync($"api/v1/organization/myOrganization/employees");

            await ResponseAssert.ListAreEquals(
                new[]
                {
                    new EmployeeModel
                    {
                        Name = "Ricardo Nunes",
                        Title = "Engineer",
                        Image = GetSampleImage().ToBase64()
                    }
                },
                response
            );
        }

        [Test]
        public async Task Getting_employees_from_an_organization_that_doesnt_belong_to_the_user_returns_401()
        {
            var client = await GetAuthenticatedClient();

            var response = await client.GetAsync($"api/v1/organization/NotThisUserOrganization/employees");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
    }
}
