using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;

namespace Cresce.Core.Customers
{
    public interface ICustomerServices
    {
        Task<IEnumerable<Customer>> GetCustomers(IEmployeeAuthorization authorization);
    }
}