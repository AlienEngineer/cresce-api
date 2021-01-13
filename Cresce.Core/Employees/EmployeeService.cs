using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;

namespace Cresce.Core.Employees
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly IGetEmployeesGateway _gateway;
        private readonly IAuthorizedUserFactory _authorizedUserFactory;

        public EmployeeService(
            IGetEmployeesGateway gateway,
            IAuthorizedUserFactory authorizedUserFactory
        )
        {
            _gateway = gateway;
            _authorizedUserFactory = authorizedUserFactory;
        }

        public async Task<IEnumerable<Employee>> GetEmployees(AuthorizedUser user, string organizationId)
        {
            await user.EnsureCanAccessOrganization(organizationId);
            return await _gateway.GetEmployees(organizationId);
        }

        public async Task<AuthorizedEmployee> ValidatePin(AuthorizedUser user, EmployeePin employeePin)
        {
            var employee = await _gateway.GetEmployeeById(employeePin.EmployeeId);

            return !employee.Verify(employeePin)
                ? _authorizedUserFactory.makeUnauthorizedEmployee()
                : _authorizedUserFactory.GetAuthorizedEmployee(user, employeePin.EmployeeId);
        }
    }
}
