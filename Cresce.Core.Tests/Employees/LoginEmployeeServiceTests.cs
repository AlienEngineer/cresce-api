using System.Threading.Tasks;
using Cresce.Core.Employees;
using NUnit.Framework;

namespace Cresce.Core.Tests.Employees
{
    public class LoginEmployeeServiceTests : ServicesTests<IEmployeeService>
    {

        [Test]
        public async Task Valid_employee_pin_returns_employee_token()
        {
            var service = MakeService();

            var authorizedUser = await service.ValidatePin(
                GetAuthorizedUser(),
                new EmployeePin { EmployeeId = "Ricardo Nunes", Pin = "1234" }
            );

            Assert.That(authorizedUser, Is.Not.Null);
            Assert.That(authorizedUser.EmployeeId, Is.EqualTo("Ricardo Nunes"));
        }

        [Test]
        public async Task Invalid_employee_pin_returns_invalid_token()
        {
            var service = MakeService();

            var authorizedUser = await service.ValidatePin(
                GetAuthorizedUser(),
                new EmployeePin { EmployeeId = "Ricardo Nunes", Pin = "4321" }
            );

            Assert.That(authorizedUser, Is.Not.Null);
            Assert.That(authorizedUser.IsExpired, Is.True);
        }
    }
}
