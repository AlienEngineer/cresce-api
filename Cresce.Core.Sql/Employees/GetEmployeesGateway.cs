using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Cresce.Core.Employees;
using Microsoft.EntityFrameworkCore;

namespace Cresce.Core.Sql.Employees
{
    internal class GetEmployeesGateway : IGetEmployeesGateway
    {
        private readonly CresceContext _context;

        public GetEmployeesGateway(CresceContext context)
        {
            _context = context;
        }
        public Task<IEnumerable<Employee>> GetEmployees(string organizationId)
        {
            return Task.FromResult(_context
                .Set<EmployeeModel>()
                .AsSingleQuery()
                .Where(e => e.OrganizationId == organizationId)
                .AsEnumerable()
                .Select(e => e.ToEmployee()));
        }

        public async Task<Employee> GetEmployeeById(string employeeId)
        {
            var model = await _context.Set<EmployeeModel>().FindAsync(employeeId);
            return model.ToEmployee();
        }
    }

    internal class EmployeeModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public byte[] Image { get; set; }
        public string OrganizationId { get; set; }
        public string Pin { get; set; }

        public Employee ToEmployee()
        {
            return new Employee
            {
                Name = Id,
                Title = Title,
                Image = new Image(Image),
                Pin = Pin
            };
        }
    }
}
