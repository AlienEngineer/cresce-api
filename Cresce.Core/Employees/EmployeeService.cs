using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.Employees.EmployeeValidation;
using Cresce.Core.Employees.GetEmployees;

namespace Cresce.Core.Employees
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly GetEmployeesService _getEmployeesService;
        private readonly EmployeeValidationService _employeeValidationService;

        public EmployeeService(
            IGetEmployeesGateway gateway,
            IAuthorizationFactory authorizationFactory
        )
        {
            _getEmployeesService = new GetEmployeesService(gateway);
            _employeeValidationService = new EmployeeValidationService(gateway, authorizationFactory);
        }

        public Task<IEnumerable<Employee>> GetEmployees(IAuthorization user, string organizationId) =>
            _getEmployeesService.GetEmployees(user, organizationId);

        public Task<IEmployeeAuthorization> ValidatePin(IAuthorization user, EmployeePin employeePin) =>
            _employeeValidationService.ValidatePin(user, employeePin);
    }
}
