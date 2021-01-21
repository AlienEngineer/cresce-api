using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;

namespace Cresce.Core.Customers.GetCustomers
{
    public interface IGetCustomersService
    {
        Task<IEnumerable<Customer>> GetCustomers(IEmployeeAuthorization authorization);
    }
}
