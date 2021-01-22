using Cresce.Core.Organizations;

namespace Cresce.Core.Sql.Organizations
{
    internal class OrganizationModel : IUnwrap<Organization>, IWrap<Organization>
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

        public void Wrap(Organization entity)
        {
            throw new System.NotImplementedException();
        }
    }
}