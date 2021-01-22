using System.Threading.Tasks;
using Cresce.Core.Authentication;
using NUnit.Framework;

namespace Cresce.Core.Tests.Authentication
{
    public class LoginServiceTests : ServicesTests<ILoginService>
    {
        [Test]
        public async Task Credentials_are_valid_when_user_and_password_match()
        {
            var service = MakeService();

            var result = await service.AreCredentialsValid(new Credentials("myUser", "myPass"));

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task Credentials_are_invalid_when_user_is_unknown()
        {
            var service = MakeService();

            var result = await service.AreCredentialsValid(new Credentials("myUser1", "myPass"));

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task Credentials_are_invalid_when_password_is_wrong()
        {
            var service = MakeService();

            var result = await service.AreCredentialsValid(new Credentials("myUser", "myPass1"));

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task When_credentials_are_valid_returns_authorization_token()
        {
            var service = MakeService();

            var token = await service.ValidateCredentials(new Credentials("myUser", "myPass"));

            Assert.That(token.IsExpired, Is.False);
        }

        [Test]
        public async Task When_credentials_have_wrong_password_returns_expired_token()
        {
            var service = MakeService();

            var token = await service.ValidateCredentials(new Credentials("myUser", "myPass1"));

            Assert.That(token.IsExpired, Is.True);
        }

        [Test]
        public async Task When_credentials_unknown_user_password_returns_expired_token()
        {
            var service = MakeService();

            var token = await service.ValidateCredentials(new Credentials("myUser1", "myPass"));

            Assert.That(token.IsExpired, Is.True);
        }
    }
}