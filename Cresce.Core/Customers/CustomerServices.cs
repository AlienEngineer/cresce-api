using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.Customers.GetCustomers;

namespace Cresce.Core.Customers
{
    public class CustomerServices : ICustomerServices
    {
        private readonly IGetCustomersService _getCustomersService;

        public CustomerServices(IGetCustomersService getCustomersService)
        {
            _getCustomersService = getCustomersService;
        }

        public Task<IEnumerable<Customer>> GetCustomers(IEmployeeAuthorization authorization) =>
            _getCustomersService.GetCustomers(authorization);
    }
}
