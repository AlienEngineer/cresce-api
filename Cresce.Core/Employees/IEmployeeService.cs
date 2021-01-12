using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;

namespace Cresce.Core.Employees
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees(AuthorizedUser user, string organizationId);
        Task<AuthorizedEmployee> ValidatePin(AuthorizedUser user, EmployeePin employeePin);
    }

    public record EmployeePin
    {
        public string EmployeeId { get; set; }
        public string Pin { get; set; }
    }
}
