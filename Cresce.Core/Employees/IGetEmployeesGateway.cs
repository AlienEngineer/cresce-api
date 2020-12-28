using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cresce.Core.Employees
{
    public interface IGetEmployeesGateway
    {
        Task<IEnumerable<Employee>> GetEmployees(string organizationId);
    }
}