using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;

namespace Cresce.Core.Customers.GetCustomers
{
    internal class GetCustomerService : IGetCustomersService
    {
        private readonly IGetCustomersGateway _gateway;

        public GetCustomerService(IGetCustomersGateway gateway) => _gateway = gateway;

        public Task<IEnumerable<Customer>> GetCustomers(IEmployeeAuthorization authorization)
        {
            authorization.EnsureIsValid();
            return _gateway.GetCustomers();
        }
    }
}
