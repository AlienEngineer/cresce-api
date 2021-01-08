using System.Threading.Tasks;
using Cresce.Core.Organizations;
using Cresce.Core.Users;
using NUnit.Framework;

namespace Cresce.Core.Sql.Tests.Organizations
{
    internal class GetUserOrganizationsGatewayTests : SqlTest<IGetUserOrganizationsGateway>
    {

        [Test]
        public async Task Getting_organization_of_a_user_returns_the_user_organizations()
        {
            var gateway = MakeGateway();

            var organizations = await gateway.GetOrganizations("myUser");

            Assert.That(organizations, Is.EqualTo(new []
            {
                new Organization { Name = "myOrganization" }
            }));
        }

        [Test]
        public async Task Getting_non_existing_user_organizations_returns_empty()
        {
            var gateway = MakeGateway();

            var organizations = await gateway.GetOrganizations("myUser1");

            Assert.That(organizations, Is.Empty);
        }

    }
}
