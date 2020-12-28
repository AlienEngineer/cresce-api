using System.Threading.Tasks;
using Cresce.Api.Models;
using Cresce.Core.Authentication;
using Cresce.Core.Employees;
using NUnit.Framework;

namespace Cresce.Core.Tests.Employees
{
    public class EmployeeServiceTests : ServicesTests<IEmployeeService>
    {
        [Test]
        public async Task Getting_employees_from_organization_returns_employees_for_given_organization()
        {
            var services = MakeService();

            var employees = await services.GetEmployees(GetAuthorizedUser(), "myOrganization");

            CollectionAssert.AreEqual(new []
            {
                new Employee
                {
                    Name = "Ricardo Nunes",
                    Title = "Engineer",
                    Image = GetSampleImage()
                },
            }, employees);
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
                services.GetEmployees(GetExpiredUser(), "myOrganization")
            );
        }
    }
}
