using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Cresce.Core.Organizations;
using NUnit.Framework;

namespace Cresce.Api.Tests
{
    public class OrganizationsControllerTests : WebApiTests
    {
        [Test]
        public async Task Getting_organization_returns_organization_dto()
        {
            var client = await GetAuthenticatedClient();

            var response = await client.GetAsync("api/v1/organization");

            await ResponseAssert.ListAreEquals(
                new [] { new Organization { Name = "myOrganization" } },
                response
            );
        }
    }

    public static class ResponseAssert
    {
        public static void AreEquals<T>(T expect, HttpResponseMessage response)
        {

        }

        public static async Task ListAreEquals<T>(T expect, HttpResponseMessage response)
            where T: IEnumerable
        {
            CollectionAssert.AreEqual(expect, await response.GetContent<T>());
        }
    }

    public static class ResponseExtensions
    {
        public static Task<T> GetContent<T>(this HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<T>();
        }
    }
}
