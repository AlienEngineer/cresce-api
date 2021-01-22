using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.Employees.GetEmployees;

namespace Cresce.Core.Employees.EmployeeValidation
{
    internal class EmployeeValidationService : IEmployeeValidationService
    {
        private readonly IGetEntityByIdGateway<Employee> _gateway;
        private readonly IAuthorizationFactory _authorizationFactory;

        public EmployeeValidationService(IGetEntityByIdGateway<Employee> gateway,
            IAuthorizationFactory authorizationFactory)
        {
            _gateway = gateway;
            _authorizationFactory = authorizationFactory;
        }

        public async Task<IEmployeeAuthorization> ValidatePin(IAuthorization user, EmployeePin employeePin)
        {
            var employee = await _gateway.GetById(employeePin.EmployeeId);

            return !employee.Verify(employeePin)
                ? _authorizationFactory.MakeExpiredEmployeeAuthorization()
                : _authorizationFactory.GetAuthorizedEmployee(user, employeePin.EmployeeId);
        }
    }
}