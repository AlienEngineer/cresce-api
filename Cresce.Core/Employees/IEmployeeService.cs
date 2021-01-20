using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.Employees.EmployeeValidation;
using Cresce.Core.Employees.GetEmployees;

namespace Cresce.Core.Employees
{
    public interface IEmployeeService : IGetEmployeesService, IEmployeeValidationService
    {
    }
}
