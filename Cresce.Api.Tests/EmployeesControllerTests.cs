using System.Threading.Tasks;
using Cresce.Core.Employees;
using NUnit.Framework;

namespace Cresce.Api.Tests
{
    public class EmployeesControllerTests : WebApiTests
    {
        [Test]
        public async Task Getting_employees_returns_employees_for_given_organization()
        {
            var client = await GetAuthenticatedClient();

            var response = await client.GetAsync($"api/v1/organization/myOrganization/employees");

            await ResponseAssert.ListAreEquals(
                new[] {new Employee {Name = "myEmployee"}},
                response
            );
        }

    }
}
