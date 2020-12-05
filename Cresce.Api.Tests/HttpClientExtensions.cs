using System.Net.Http;
using System.Threading.Tasks;
using Cresce.Api.Controllers.Authentications;

namespace Cresce.Api.Tests
{
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