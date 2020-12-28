using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;

namespace Cresce.Core.Employees
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees(AuthorizedUser user, string organizationId);
    }
}
