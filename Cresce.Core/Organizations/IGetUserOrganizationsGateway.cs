using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cresce.Core.Organizations
{
    public interface IGetUserOrganizationsGateway
    {
        Task<IEnumerable<Organization>> GetOrganizations(string userid);
    }
}