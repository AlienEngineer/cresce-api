using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Organizations;

namespace Cresce.Core.InMemory.Organizations
{
    internal class GetUserOrganizationsGateway : IGetUserOrganizationsGateway
    {
        private readonly IReadOnlyDictionary<string, object> _dictionary;

        public GetUserOrganizationsGateway(IReadOnlyDictionary<string, object> dictionary)
        {
            _dictionary = dictionary;
        }

        public Task<IEnumerable<Organization>> GetOrganizations(string userId)
        {
            _dictionary.TryGetValue($"user_{userId}_organizations", out var data);
            return Task.FromResult(data as IEnumerable<Organization> ?? new Organization[0]);
        }
    }
}
