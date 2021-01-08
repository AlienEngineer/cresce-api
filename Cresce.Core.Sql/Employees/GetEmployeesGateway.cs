using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Employees;

namespace Cresce.Core.Sql.Employees
{
    internal class GetEmployeesGateway : IGetEmployeesGateway
    {
        public Task<IEnumerable<Employee>> GetEmployees(string organizationId)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class EmployeeModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
    }
}
