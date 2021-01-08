using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Organizations;

namespace Cresce.Core.Sql.Organizations
{
    internal class GetUserOrganizationsGateway : IGetUserOrganizationsGateway
    {
        public Task<IEnumerable<Organization>> GetOrganizations(string userid)
        {
            throw new System.NotImplementedException();
        }
    }
}
