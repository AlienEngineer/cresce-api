using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;

namespace Cresce.Core.Services.GetServices
{
    public interface IGetServicesService
    {
        Task<IEnumerable<Service>> GetServices(IEmployeeAuthorization authorization);
    }
}
