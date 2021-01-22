using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.GetEntities;

namespace Cresce.Core.Employees.GetEmployees
{
    public interface IGetEmployeesGateway
    {
        Task<IEnumerable<Employee>> GetEmployees(string organizationId);
        Task<Employee> GetEmployeeById(string employeeId);

    }
}
