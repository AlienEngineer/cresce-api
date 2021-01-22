using System.Net.Http;
using System.Threading.Tasks;
using Cresce.Api.Controllers.Authentications;
using Cresce.Api.Controllers.Employees;
using Cresce.Core.Employees.EmployeeValidation;

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

        public static async Task<EmployeeLoginResultDto> LoginEmployee(this HttpClient client)
        {
            var response = await client.PostAsJsonAsync("/api/v1/employees/", new EmployeePin
            {
                EmployeeId = "Ricardo Nunes",
                Pin = "1234"
            });

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<EmployeeLoginResultDto>();
        }
    }
}