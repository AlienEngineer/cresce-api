using Cresce.Core.Employees.GetEmployees;

namespace Cresce.Core.Employees
{
    public interface IEmployeeService : IGetEmployeesService, IEmployeeValidationService
    {
    }
}
