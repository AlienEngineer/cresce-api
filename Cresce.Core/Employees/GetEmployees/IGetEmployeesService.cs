using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;

namespace Cresce.Core.Employees.GetEmployees
{
    public interface IGetEmployeesService
    {
        Task<IEnumerable<Employee>> GetEmployees(IAuthorization authorization, string organizationId);
    }
}