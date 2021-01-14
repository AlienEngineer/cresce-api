using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;

namespace Cresce.Core.Employees
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly IGetEmployeesGateway _gateway;
        private readonly IAuthorizationFactory _authorizationFactory;

        public EmployeeService(
            IGetEmployeesGateway gateway,
            IAuthorizationFactory authorizationFactory
        )
        {
            _gateway = gateway;
            _authorizationFactory = authorizationFactory;
        }

        public async Task<IEnumerable<Employee>> GetEmployees(IAuthorization user, string organizationId)
        {
            await user.EnsureCanAccessOrganization(organizationId);
            return await _gateway.GetEmployees(organizationId);
        }

        public async Task<IEmployeeAuthorization> ValidatePin(IAuthorization user, EmployeePin employeePin)
        {
            var employee = await _gateway.GetEmployeeById(employeePin.EmployeeId);

            return !employee.Verify(employeePin)
                ? _authorizationFactory.MakeUnauthorizedEmployee()
                : _authorizationFactory.GetAuthorizedEmployee(user, employeePin.EmployeeId);
        }
    }
}
