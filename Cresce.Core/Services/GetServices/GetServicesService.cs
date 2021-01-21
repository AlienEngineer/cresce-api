using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;

namespace Cresce.Core.Services.GetServices
{
    internal class GetServicesService : IGetServicesService
    {
        private readonly IGetServicesGateway _gateway;

        public GetServicesService(IGetServicesGateway gateway) => _gateway = gateway;

        public Task<IEnumerable<Service>> GetServices(IEmployeeAuthorization authorization)
        {
            authorization.EnsureIsValid();
            return _gateway.GetServices();
        }
    }
}
