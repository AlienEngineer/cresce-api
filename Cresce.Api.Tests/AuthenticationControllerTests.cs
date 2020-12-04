using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cresce.Api.Controllers.Authentications;
using NUnit.Framework;

namespace Cresce.Api.Tests
{
    public class AuthenticationsControllerTests : WebApiTests
    {

        [Test]
        public async Task Verifying_valid_login_credentials_returns_200()
        {
            var client = GetClient();

            var loginResult = await client.Login();

            Assert.That(loginResult, Is.Not.Null);
            Assert.That(loginResult.OrganizationUrl, Is.EqualTo("api/v1/organization/"));
            Assert.That(loginResult.Token, Is.Not.Null);
        }

        [Test]
        public async Task Verifying_invalid_login_credentials_returns_401()
        {
            var client = GetClient();

            var response = await client.PostAsJsonAsync("/api/v1/authentication/", new CredentialsDto
            {
                User = "myUser1",
                Password = "myPass1"
            });

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

    }

    internal static class HttpClientExtensions
    {
        public static async Task<LoginResultDto> Login(this HttpClient client)
        {
            var response = await client.PostAsJsonAsync("/api/v1/authentication/", new CredentialsDto
            {
                User = "myUser",
                Password = "myPass"
            });

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<LoginResultDto>();
        }
    }
}
