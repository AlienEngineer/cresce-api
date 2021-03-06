using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;

namespace Cresce.Core.Employees.GetEmployees
{
    internal class GetEmployeesService : IGetEmployeesService
    {
        private readonly IGetEmployeesGateway _gateway;

        public GetEmployeesService(IGetEmployeesGateway gateway) => _gateway = gateway;

        public async Task<IEnumerable<Employee>> GetEmployees(IAuthorization authorization, string organizationId)
        {
            await authorization.EnsureCanAccessOrganization(organizationId);
            return await _gateway.GetEmployees(organizationId);
        }

        public Task<IEnumerable<Employee>> GetEntities(IEmployeeAuthorization authorization) =>
            GetEmployees(authorization, authorization.OrganizationId);
    }
}
