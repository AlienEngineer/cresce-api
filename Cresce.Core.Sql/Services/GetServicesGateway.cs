using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cresce.Core.Services.GetServices;
using Microsoft.EntityFrameworkCore;

namespace Cresce.Core.Sql.Services
{
    internal class GetServicesGateway : IGetServicesGateway
    {
        private readonly CresceContext _context;

        public GetServicesGateway(CresceContext context) => _context = context;

        public async Task<IEnumerable<Service>> GetServices()
        {
            var employeesModels = await _context
                .Set<ServiceModel>()
                .ToListAsync();

            return employeesModels.Select(e => e.ToService());
        }
    }
}
