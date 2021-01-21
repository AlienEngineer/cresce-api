using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;

namespace Cresce.Core.Services
{
    public interface IServiceServices
    {
        Task<IEnumerable<Service>> GetServices(IEmployeeAuthorization authorization);
    }
}
