using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Employees;

namespace Cresce.Core.InMemory.Employees
{
    internal class GetEmployeesGateway : IGetEmployeesGateway
    {
        private readonly IReadOnlyDictionary<string, object> _dictionary;

        public GetEmployeesGateway(IReadOnlyDictionary<string, object> dictionary)
        {
            _dictionary = dictionary;
        }

        public Task<IEnumerable<Employee>> GetEmployees(string organizationId)
        {
            _dictionary.TryGetValue($"organization_{organizationId}_employees", out var data);
            return Task.FromResult(data as IEnumerable<Employee> ?? new Employee[0]);
        }
    }
}
