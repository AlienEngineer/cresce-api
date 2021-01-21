using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cresce.Core.Customers.GetCustomers;
using Cresce.Core.Sql.Services;
using Microsoft.EntityFrameworkCore;

namespace Cresce.Core.Sql.Customers
{
    internal class GetCustomersGateway : IGetCustomersGateway
    {
        private readonly CresceContext _context;

        public GetCustomersGateway(CresceContext context) => _context = context;

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var employeesModels = await _context
                .Set<CustomerModel>()
                .ToListAsync();

            return employeesModels.Select(e => e.ToCustomer());
        }
    }
}
