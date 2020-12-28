using System.Threading.Tasks;
using Cresce.Core.Authentication;
using Cresce.Core.Organizations;
using NUnit.Framework;

namespace Cresce.Core.Tests.Organizations
{
    public class OrganizationServiceTests : ServicesTests<IOrganizationService>
    {
        [Test]
        public async Task Getting_organizations_for_user_token_returns_organizations()
        {
            var service = MakeService();

            var organizations = await service.GetOrganizations(GetAuthorizedUser());

            CollectionAssert.AreEqual(new[]
            {
                new Organization { Name = "myOrganization" }
            }, organizations);
        }

        [Test]
        public async Task Getting_organizations_for_unknown_user_returns_empty_list()
        {
            var service = MakeService();

            var organizations = await service.GetOrganizations(GetUnknownUser());

            CollectionAssert.IsEmpty(organizations);
        }

        [Test]
        public void When_user_is_unauthorized_throws_exception()
        {
            var service = MakeService();

            Assert.CatchAsync<UnauthorizedException>(() =>
                service.GetOrganizations(GetExpiredUser())
            );
        }

    }

}
