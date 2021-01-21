using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cresce.Core.Customers.GetCustomers
{
    public interface IGetCustomersGateway
    {
        Task<IEnumerable<Customer>> GetCustomers();
    }
}
