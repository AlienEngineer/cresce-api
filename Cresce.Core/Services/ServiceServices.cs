using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.GetEntities;

namespace Cresce.Core.Services
{
    public class ServiceServices : IServiceServices
    {
        private readonly IGetEntitiesService<Service> _getServicesService;

        public ServiceServices(IGetEntitiesService<Service> getServicesService)
        {
            _getServicesService = getServicesService;
        }

        public Task<IEnumerable<Service>> GetServices(IEmployeeAuthorization authorization) =>
            _getServicesService.GetEntities(authorization);
    }
}