using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cresce.Core.Employees;
using Cresce.Core.Employees.GetEmployees;
using Microsoft.EntityFrameworkCore;

namespace Cresce.Core.Sql.Employees
{
    internal class GetEmployeesGateway : IGetEmployeesGateway
    {
        private readonly CresceContext _context;

        public GetEmployeesGateway(CresceContext context) => _context = context;

        public async Task<IEnumerable<Employee>> GetEmployees(string organizationId)
        {
            var employeesModels = await _context
                .Set<EmployeeModel>()
                .Where(e => e.OrganizationId == organizationId)
                .ToListAsync();

            return employeesModels.Select(e => e.ToEmployee());
        }

        public async Task<Employee> GetEmployeeById(string employeeId)
        {
            var model = await _context.Set<EmployeeModel>().FindAsync(employeeId) ?? new EmployeeModel();
            return model.ToEmployee();
        }
    }
}
