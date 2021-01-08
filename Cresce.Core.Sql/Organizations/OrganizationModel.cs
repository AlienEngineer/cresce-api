using Cresce.Core.Organizations;

namespace Cresce.Core.Sql.Organizations
{
    internal class OrganizationModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public Organization ToOrganization()
        {
            return new Organization
            {
                Name = Id
            };
        }
    }
}
