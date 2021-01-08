using System.Threading.Tasks;
using Cresce.Core.Users;
using NUnit.Framework;

namespace Cresce.Core.Sql.Tests.Users
{
    internal class GetUserGatewayTests : SqlTest<IGetUserGateway>
    {

        [Test]
        public async Task Getting_user_by_id_returns_the_user()
        {
            var gateway = MakeGateway();

            var user = await gateway.GetUser("myUser");

            Assert.That(user, Is.EqualTo(new AdminUser
            {
                Id = "myUser",
                Password = "myPass"
            }));
        }

        [Test]
        public async Task Getting_non_existing_user_returns_unknown_user()
        {
            var gateway = MakeGateway();

            var user = await gateway.GetUser("myUser1");

            Assert.That(user, Is.EqualTo(new UnknownUser()));
        }

    }
}
