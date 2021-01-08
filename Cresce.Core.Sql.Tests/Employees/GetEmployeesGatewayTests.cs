using System.Threading.Tasks;
using Cresce.Core.Employees;
using NUnit.Framework;

namespace Cresce.Core.Sql.Tests.Employees
{
    internal class GetEmployeesGatewayTests : SqlTest<IGetEmployeesGateway>
    {

        [Test]
        public async Task Getting_employees_of_a_organization_returns_the_organization_employees()
        {
            var gateway = MakeGateway();

            var employees = await gateway.GetEmployees("myOrganization");

            Assert.That(employees, Is.EqualTo(new []
            {
                new Employee
                {
                    Name = "Ricardo Nunes",
                    Title = "Engineer",
                    Image = new Image(GetSampleImage())
                }
            }));
        }

        [Test]
        public async Task Getting_non_existing_organization_employees_returns_empty()
        {
            var gateway = MakeGateway();

            var employees = await gateway.GetEmployees("myOrganization1");

            Assert.That(employees, Is.Empty);
        }

    }
}
