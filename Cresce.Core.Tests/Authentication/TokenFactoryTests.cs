using Cresce.Core.Authentication;
using Cresce.Core.Users;
using NUnit.Framework;

namespace Cresce.Core.Tests.Authentication
{
    public class TokenFactoryTests : ServicesTests<IAuthorizationFactory>
    {
        [Test]
        public void Making_a_token_from_a_user_it_is_not_expired()
        {
            var factory = MakeService();

            var token = factory.MakeAuthorization(new AdminUser {Id = "myUser"});

            Assert.That(token.IsExpired, Is.False);
        }

        [Test]
        public void Roundtrip_token_from_string_returns_valid_token()
        {
            var factory = MakeService();

            var token = factory.MakeAuthorization(new AdminUser {Id = "myUser"});
            var roundTripToken = factory.DecodeAuthorization(token.ToString()!);

            Assert.Multiple(() =>
            {
                Assert.That(token.IsExpired, Is.False);
                Assert.That(roundTripToken.IsExpired, Is.False);
            });
        }

        [Test]
        public void Decode_invalid_token_from_string_returns_expired_token()
        {
            var factory = MakeService();

            var token = factory.DecodeAuthorization("Invalid Token");

            Assert.That(token.IsExpired, Is.True);
        }

        [Test]
        public void Getting_user_from_invalid_token_throws_exception()
        {
            var factory = MakeService();

            var token = factory.DecodeAuthorization("Invalid Token");

            Assert.That(token.IsExpired, Is.True);
            var exception = Assert.Catch<UnauthorizedException>(() =>
                Assert.That(token.UserId, Is.Not.Null)
            );

            Assert.That(exception.Message, Is.EqualTo("Authorization has expired."));
        }

        [Test]
        public void Getting_user_from_valid_token_returns_the_user_id()
        {
            var factory = MakeService();

            var token = factory.MakeAuthorization(new AdminUser {Id = "myUser"});

            Assert.Multiple(() =>
            {
                Assert.That(token.IsExpired, Is.False);
                Assert.That(token.UserId, Is.EqualTo("myUser"));
            });
        }

        [Test]
        public void Getting_admin_user_from_valid_token_returns_the_user_role_as_admin()
        {
            var factory = MakeService();

            var token = factory.MakeAuthorization(new AdminUser {Id = "myUser"});

            Assert.Multiple(() =>
            {
                Assert.That(token.IsExpired, Is.False);
                Assert.That(token.UserId, Is.EqualTo("myUser"));
                Assert.That(token.Role, Is.EqualTo("Admin"));
            });
        }
    }
}