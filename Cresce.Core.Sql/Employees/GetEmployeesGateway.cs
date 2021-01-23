using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Employees.GetEmployees;
using Cresce.Core.Sql.GetEntities;

namespace Cresce.Core.Sql.Employees
{
    internal class GetEmployeesGateway : IGetEmployeesGateway
    {
        private readonly IGetEntitiesQuery<EmployeeDto, Employee> _entitiesQuery;

        public GetEmployeesGateway(IGetEntitiesQuery<EmployeeDto, Employee> entitiesQuery) =>
            _entitiesQuery = entitiesQuery;

        public Task<IEnumerable<Employee>> GetEmployees(string organizationId) =>
            _entitiesQuery.GetEntities(filter: e => e.OrganizationId == organizationId);
    }
}