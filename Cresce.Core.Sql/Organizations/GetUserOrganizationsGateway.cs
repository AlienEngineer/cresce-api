using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cresce.Core.Organizations;
using Microsoft.EntityFrameworkCore;

namespace Cresce.Core.Sql.Organizations
{
    internal class GetUserOrganizationsGateway : IGetUserOrganizationsGateway
    {
        private readonly CresceContext _context;

        public GetUserOrganizationsGateway(CresceContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Organization>> GetOrganizations(string userid)
        {
            return Task.FromResult(_context
                .Set<OrganizationModel>()
                .AsSingleQuery()
                .Where(e => e.UserId == userid)
                .AsEnumerable()
                .Select(e => e.ToOrganization()));
        }
    }
}
