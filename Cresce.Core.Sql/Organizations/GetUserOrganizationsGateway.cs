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
        private readonly IGetEntitiesQuery<OrganizationModel, Organization> _entitiesQuery;

        public GetUserOrganizationsGateway(
            CresceContext context,
            IGetEntitiesQuery<OrganizationModel, Organization> entitiesQuery
        )
        {
            _context = context;
            _entitiesQuery = entitiesQuery;
        }

        public Task<IEnumerable<Organization>> GetOrganizations(string userid) =>
            _entitiesQuery.GetEntities(filter: e => e.UserId == userid);

    }
}
