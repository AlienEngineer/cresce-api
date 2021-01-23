using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cresce.Core.Organizations;
using Cresce.Core.Sql.GetEntities;
using Microsoft.EntityFrameworkCore;

namespace Cresce.Core.Sql.Organizations
{
    internal class GetUserOrganizationsGateway : IGetUserOrganizationsGateway
    {
        private readonly IGetEntitiesQuery<OrganizationDto, Organization> _entitiesQuery;

        public GetUserOrganizationsGateway(
            IGetEntitiesQuery<OrganizationDto, Organization> entitiesQuery
        )
        {
            _entitiesQuery = entitiesQuery;
        }

        public Task<IEnumerable<Organization>> GetOrganizations(string userid) =>
            _entitiesQuery.GetEntities(filter: e => e.UserId == userid);
    }
}