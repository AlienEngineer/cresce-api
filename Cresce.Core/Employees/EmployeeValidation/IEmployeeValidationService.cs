using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.Employees.EmployeeValidation;

namespace Cresce.Core.Employees
{
    public interface IEmployeeValidationService
    {
        Task<IEmployeeAuthorization> ValidatePin(IAuthorization user, EmployeePin employeePin);
    }
}
