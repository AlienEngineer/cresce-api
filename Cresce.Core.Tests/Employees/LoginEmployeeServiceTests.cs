using System.Threading.Tasks;
using Cresce.Core.Employees;
using Cresce.Core.Employees.EmployeeValidation;
using NUnit.Framework;

namespace Cresce.Core.Tests.Employees
{
    public class LoginEmployeeServiceTests : ServicesTests<IEmployeeService>
    {
        [Test]
        public async Task Valid_employee_pin_returns_employee_token()
        {
            var service = MakeService();

            var authorization = await service.ValidatePin(
                GetAuthorization(),
                new EmployeePin {EmployeeId = 1, Pin = "1234"}
            );

            Assert.That(authorization, Is.Not.Null);
            Assert.That(authorization.EmployeeId, Is.EqualTo(1));
            Assert.That(authorization.IsExpired, Is.False);
        }

        [Test]
        public async Task Invalid_employee_pin_returns_invalid_token()
        {
            var service = MakeService();

            var authorization = await service.ValidatePin(
                GetAuthorization(),
                new EmployeePin {EmployeeId = 1, Pin = "4321"}
            );

            Assert.That(authorization, Is.Not.Null);
            Assert.That(authorization.IsExpired, Is.True);
        }

        [Test]
        public async Task Unknown_employee_returns_invalid_token()
        {
            var service = MakeService();

            var authorization = await service.ValidatePin(
                GetAuthorization(),
                new EmployeePin {EmployeeId = -1, Pin = "1234"}
            );

            Assert.That(authorization, Is.Not.Null);
            Assert.That(authorization.IsExpired, Is.True);
        }
    }
}