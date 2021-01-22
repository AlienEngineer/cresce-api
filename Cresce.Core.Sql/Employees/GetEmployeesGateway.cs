using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Employees.GetEmployees;
using Cresce.Core.Sql.GetEntities;

namespace Cresce.Core.Sql.Employees
{
    internal class GetEmployeesGateway : IGetEmployeesGateway
    {
        private readonly IGetEntitiesQuery<EmployeeModel, Employee> _entitiesQuery;
        private readonly IGetEntityById<Employee> _byIdQuery;

        public GetEmployeesGateway(
            IGetEntitiesQuery<EmployeeModel, Employee> entitiesQuery,
            IGetEntityById<Employee> byIdQuery
        )
        {
            _entitiesQuery = entitiesQuery;
            _byIdQuery = byIdQuery;
        }

        public Task<IEnumerable<Employee>> GetEmployees(string organizationId) =>
            _entitiesQuery.GetEntities(filter: e => e.OrganizationId == organizationId);

        public Task<Employee> GetEmployeeById(string employeeId) =>
            _byIdQuery.GetById(employeeId);
    }
}
