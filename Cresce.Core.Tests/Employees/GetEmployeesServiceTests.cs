using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.Employees;
using Cresce.Core.Employees.GetEmployees;
using NUnit.Framework;

namespace Cresce.Core.Tests.Employees
{
    public class GetEmployeesServiceTests : ServicesTests<IEmployeeService>
    {
        [Test]
        public async Task Getting_employees_from_organization_returns_employees_for_given_organization()
        {
            var services = MakeService();

            var entities = await services.GetEmployees(GetAuthorization(), "myOrganization");

            CollectionAssert.AreEqual(new[]
            {
                new Employee
                {
                    Id = 1,
                    Name = "Ricardo Nunes",
                    Title = "Engineer",
                    Image = GetSampleImage(),
                    Pin = "1234",
                    Color = "0xFF2196F3",
                    OrganizationId = "myOrganization",
                },
            }, entities);
        }

        [Test]
        public void Getting_employees_with_invalid_authentication_throws_exception()
        {
            var services = MakeService();

            Assert.CatchAsync<UnauthorizedException>(() =>
                services.GetEmployees(GetUnknownUser(), "myOrganization")
            );
        }

        [Test]
        public void Getting_employees_with_expired_authentication_throws_exception()
        {
            var services = MakeService();

            Assert.CatchAsync<UnauthorizedException>(() =>
                services.GetEmployees(GetExpiredAuthorization(), "myOrganization")
            );
        }
    }
}
