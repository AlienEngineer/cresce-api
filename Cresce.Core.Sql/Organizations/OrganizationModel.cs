using Cresce.Core.Organizations;

namespace Cresce.Core.Sql.Organizations
{
    internal class OrganizationModel : IUnwrap<Organization>
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public Organization Unwrap()
        {
            return new Organization
            {
                Name = Id
            };
        }
    }
}
