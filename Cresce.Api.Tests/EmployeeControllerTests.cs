using System.Threading.Tasks;
using Cresce.Core.Employees;
using NUnit.Framework;

namespace Cresce.Api.Tests
{
    public class EmployeesControllerTests : WebApiTests
    {
        [Test]
        public async Task Getting_organization_returns_organization_dto()
        {
            var client = GetClient();

            var response = await client.GetAsync($"api/v1/myUser/organization/myOrganization/employees");

            await ResponseAssert.ListAreEquals(
                new[] {new Employee {Name = "myEmployee"}},
                response
            );
        }

    }
}
