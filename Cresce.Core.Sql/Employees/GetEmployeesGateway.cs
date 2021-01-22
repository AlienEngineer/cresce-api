using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Employees.GetEmployees;

namespace Cresce.Core.Sql.Employees
{
    internal class GetEmployeesGateway : IGetEmployeesGateway
    {
        private readonly CresceContext _context;
        private readonly IGetEntitiesQuery<EmployeeModel, Employee> _entitiesQuery;

        public GetEmployeesGateway(
            CresceContext context,
            IGetEntitiesQuery<EmployeeModel, Employee> entitiesQuery
        )
        {
            _context = context;
            _entitiesQuery = entitiesQuery;
        }

        public Task<IEnumerable<Employee>> GetEmployees(string organizationId) =>
            _entitiesQuery.GetEntities(filter: e => e.OrganizationId == organizationId);

        public async Task<Employee> GetEmployeeById(string employeeId)
        {
            var model = await _context.Set<EmployeeModel>().FindAsync(employeeId) ?? new EmployeeModel();
            return model.Unwrap();
        }
    }
}
