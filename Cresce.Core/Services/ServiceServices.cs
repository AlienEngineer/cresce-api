using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.Services.GetServices;

namespace Cresce.Core.Services
{
    public class ServiceServices : IServiceServices
    {
        private readonly IGetServicesService _getServicesService;

        public ServiceServices(IGetServicesService getServicesService)
        {
            _getServicesService = getServicesService;
        }

        public Task<IEnumerable<Service>> GetServices(IEmployeeAuthorization authorization) =>
            _getServicesService.GetServices(authorization);
    }
}
