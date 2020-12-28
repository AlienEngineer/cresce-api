using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;

namespace Cresce.Core.Employees
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly IGetEmployeesGateway _gateway;

        public EmployeeService(
            IGetEmployeesGateway gateway
        )
        {
            _gateway = gateway;
        }

        public async Task<IEnumerable<Employee>> GetEmployees(AuthorizedUser user, string organizationId)
        {
            await user.EnsureCanAccessOrganization(organizationId);
            return await _gateway.GetEmployees(organizationId);
        }

    }
}
