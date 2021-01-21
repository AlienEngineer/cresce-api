using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.Employees.EmployeeValidation;
using Cresce.Core.Employees.GetEmployees;

namespace Cresce.Core.Employees
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly IGetEmployeesService _getEmployeesService;
        private readonly IEmployeeValidationService _employeeValidationService;

        public EmployeeService(
            IGetEmployeesService getEmployeesService,
            IEmployeeValidationService employeeValidationService
        )
        {
            _getEmployeesService = getEmployeesService;
            _employeeValidationService = employeeValidationService;
        }


        public Task<IEnumerable<Employee>> GetEmployees(IAuthorization authorization, string organizationId) =>
            _getEmployeesService.GetEmployees(authorization, organizationId);

        public Task<IEmployeeAuthorization> ValidatePin(IAuthorization user, EmployeePin employeePin) =>
            _employeeValidationService.ValidatePin(user, employeePin);
    }
}
