using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;

namespace Cresce.Core.Organizations
{
    public class OrganizationService: IOrganizationService
    {
        private readonly IGetUserOrganizationsGateway _gateway;

        public OrganizationService(IGetUserOrganizationsGateway gateway)
        {
            _gateway = gateway;
        }

        public Task<IEnumerable<Organization>> GetOrganizations(AuthorizedUser user)
        {
            return _gateway.GetOrganizations(user.UserId);
        }
    }


}
