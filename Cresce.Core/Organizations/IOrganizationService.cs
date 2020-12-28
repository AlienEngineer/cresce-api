using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;

namespace Cresce.Core.Organizations
{
    public interface IOrganizationService
    {
        Task<IEnumerable<Organization>> GetOrganizations(AuthorizedUser authorizedUser);
    }
}
