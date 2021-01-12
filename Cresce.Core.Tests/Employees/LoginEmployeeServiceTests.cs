using Cresce.Core.Employees;
using NUnit.Framework;

namespace Cresce.Core.Tests.Employees
{
    public class LoginEmployeeServiceTests : ServicesTests<IEmployeeService>
    {

        [Test]
        public void Valid_employee_pin_returns_employee_token()
        {
            var service = MakeService();

            service.ValidatePin();
        }

    }
}
