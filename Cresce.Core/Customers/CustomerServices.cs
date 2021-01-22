using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.GetEntities;

namespace Cresce.Core.Customers
{
    public class CustomerServices : ICustomerServices
    {
        private readonly IGetEntitiesService<Customer> _getCustomersService;

        public CustomerServices(IGetEntitiesService<Customer> getCustomersService)
        {
            _getCustomersService = getCustomersService;
        }

        public Task<IEnumerable<Customer>> GetCustomers(IEmployeeAuthorization authorization) =>
            _getCustomersService.GetEntities(authorization);
    }
}