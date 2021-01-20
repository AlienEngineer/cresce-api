using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cresce.Core.Services.GetServices
{
    public interface IGetServicesGateway
    {
        Task<IEnumerable<Service>> GetServices();
    }
}
